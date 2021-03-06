﻿using CsvHelper;
using Highlander.Data;
using Highlander.Data.Models;
using Highlander.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Http;
using System;
using Highlander.Web.Helpers;

namespace Highlander.Web.Controllers
{
    public class AccountController : BaseController
    {
        private SignInManager<ApplicationUser> _signManager;
        private UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        private IEnumerable<Title> titles = new List<Title>
        {
            new Title(){ Id = 1, Value = "Mr"},
            new Title(){ Id = 2, Value = "Mrs"},
            new Title(){ Id = 3, Value = "Miss"},
            new Title(){ Id = 4, Value = "Ms."}
        };

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signManager, ApplicationDbContext context) : base(context)
        {
            _userManager = userManager;
            _signManager = signManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterAccountViewModel()
            {
                Decorations = _context.Decorations.Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList(),
                Titles = titles.Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Value
                })
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Title = titles.FirstOrDefault(x => x.Id == model.TitleId).Value,
                    Forename = model.Forename,
                    Initial = model.Initial,
                    Surname = model.Surname,
                    DecorationId = model.DecorationId,
                    AddressLine1 = model.AddressLine1,
                    AddressLine2 = model.AddressLine2,
                    AddressLine3 = model.AddressLine3,
                    County = model.County,
                    Postcode = model.Postcode,
                    Country = model.Country,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    UserName = model.Email,
                    IsNewsletterSubscriber = model.IsNewsletterSubscriber
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                        model = new RegisterAccountViewModel()
                        {
                            Decorations = _context.Decorations.Select(x => new SelectListItem()
                            {
                                Value = x.Id.ToString(),
                                Text = x.Name
                            }).ToList(),
                            Titles = titles.Select(x => new SelectListItem()
                            {
                                Value = x.Id.ToString(),
                                Text = x.Value
                            })
                        };
                    }
                }
            }
            var errors = ModelState.Where(x => x.Value.Errors.Any())
                .Select(x => new { x.Key, x.Value.Errors });
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            var model = new LoginViewModel() { ReturnUrl = ReturnUrl };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                var result = await _signManager.PasswordSignInAsync(model.Username, model.Password, false, false);

                if (result.Succeeded)
                {
                    if (model.ReturnUrl != null)
                    {
                        return Redirect(model.ReturnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "Either your username or password is incorrect.");
                    return View(model);
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                // empty fields
                if (model.Username == null)
                {
                    ModelState.AddModelError("Username", "Username is required");
                }
                if (model.Password == null)
                {
                    ModelState.AddModelError("Password", "Password is required");
                }
                return View(model);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Manage()
        {
            var model = new EditAccountViewModel()
            {
                User = await _userManager.GetUserAsync(this.User),
                Decorations = _context.Decorations.Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToList(),
                Titles = titles.Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Value
                })
            };
            model.DecorationId = model.User.DecorationId;
            model.TitleId = model.TitleId;

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditAccountViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.User.Id.ToString());

            user.Forename = model.User.Forename;
            user.Initial = model.User.Initial;
            user.Surname = model.User.Surname;
            user.AddressLine1 = model.User.AddressLine1;
            user.AddressLine2 = model.User.AddressLine2;
            user.AddressLine3 = model.User.AddressLine3;
            user.County = model.User.County;
            user.Postcode = model.User.Postcode;
            user.Country = model.User.Country;
            user.Email = model.User.Email;
            user.UserName = model.User.Email;
            user.IsNewsletterSubscriber = model.User.IsNewsletterSubscriber;
            user.DecorationId = model.DecorationId;
            user.Title = titles.FirstOrDefault(x => x.Id == model.TitleId).Value;

            await _userManager.UpdateAsync(user);
            _context.SaveChanges();
            return RedirectToAction("Manage");
        }

        [HttpGet]
        [Route("Account/Manage/PhoneNumbers")]
        [Authorize]
        public async Task<IActionResult> PhoneNumbers()
        {
            var model = new PhoneNumbersViewModel()
            {
                User = await _userManager.GetUserAsync(this.User)
            };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> StorePhoneNumbers(PhoneNumbersViewModel model)
        {
            // persist phone numbers
            var user = await _userManager.FindByIdAsync(model.User.Id.ToString());

            user.PhoneNumber = model.User.PhoneNumber;
            user.MobileTelNo = model.User.MobileTelNo;

            await _userManager.UpdateAsync(user);
            _context.SaveChanges();

            return RedirectToAction("PhoneNumbers");
        }

        [HttpGet]
        [Authorize]
        [Route("Account/Manage/PersonalData")]
        public IActionResult PersonalData()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [Route("Account/Manage/DownloadPersonalData")]
        public async Task<FileStreamResult> DownloadPersonalData()
        {
            // get user
            var user = await _userManager.GetUserAsync(this.User);
            var applicationUser = _context.Users
                .Include(x => x.Member)
                .Include(x => x.Staff)
                    .ThenInclude(staff => staff.EmergencyContact)
                .Include(x => x.Decoration)
                .Include(x => x.Regimental)
                    .ThenInclude(regimental => regimental.Regiment)
                .Include(x => x.Volunteer)
                    .ThenInclude(volunteer => volunteer.Expertise)
                .Include(x => x.Volunteer)
                    .ThenInclude(volunteer => volunteer.EmergencyContact)
                .FirstOrDefault(x => x.Id == user.Id);

            var personalData = new PersonalData()
            {
                Title = applicationUser.Title,
                Forename = applicationUser.Forename,
                Initial = applicationUser.Initial,
                Surname = applicationUser.Surname,
                Decoration = applicationUser.Decoration.Name,
                AddressLine1 = applicationUser.AddressLine1,
                AddressLine2 = applicationUser.AddressLine2,
                AddressLine3 = applicationUser.AddressLine3,
                County = applicationUser.County,
                Country = applicationUser.Country,
                Postcode = applicationUser.Postcode,
                PhoneNumber = applicationUser.PhoneNumber,
                MobileTelNo = applicationUser.MobileTelNo,
                Email = applicationUser.Email,
                WorkEmail = applicationUser.WorkEmail,
                SubscribedToNewsLetter = applicationUser.IsNewsletterSubscriber ? "Yes" : "No",
            };

            if (applicationUser.Member != null)
            {
                personalData.MemberType = applicationUser.Member.Type;
                personalData.MemberNumber = applicationUser.Member.Number;
                personalData.MemberStartDate = applicationUser.Member.StartDate;
                personalData.MemberExpiryDate = applicationUser.Member.ExpiryDate;
            }

            if (applicationUser.Staff != null)
            {
                personalData.EmergencyContactName = applicationUser.Staff.EmergencyContact.Name;
                personalData.EmergencyContactRelation = applicationUser.Staff.EmergencyContact.Relation;
                personalData.EmergencyContactTelNo = applicationUser.Staff.EmergencyContact.TelNo;
                personalData.EndOfEmployment = applicationUser.Staff.LeaveDate ?? null;
            }

            if (applicationUser.Volunteer != null)
            {
                // override staff emergency contact
                personalData.EmergencyContactName = applicationUser.Volunteer.EmergencyContact.Name;
                personalData.EmergencyContactRelation = applicationUser.Volunteer.EmergencyContact.Relation;
                personalData.EmergencyContactTelNo = applicationUser.Volunteer.EmergencyContact.TelNo;

                personalData.Expertise = applicationUser.Volunteer.Expertise.Name;
            }

            if (applicationUser.Regimental != null)
            {
                personalData.Regiment = applicationUser.Regimental.Regiment.Name;
            }

            List<PersonalData> list = new List<PersonalData>()
            {
                personalData
            };

            var result = CsvUtilities.WriteCsvToMemory(list);
            var memoryStream = new MemoryStream(result);
            return new FileStreamResult(memoryStream, "text/csv") { FileDownloadName = "export.csv" };
        }

        [HttpGet]
        [Route("Account/Manage/Emails")]
        [Authorize]
        public async Task<IActionResult> Emails()
        {
            var model = new EmailsViewModel()
            {
                User = await _userManager.GetUserAsync(this.User)
            };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> StoreEmails(EmailsViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.User.Id.ToString());

            user.Email = model.User.Email;
            user.NormalizedEmail = model.User.Email.ToUpper();

            // also update username since its the same (subject to change)
            user.UserName = model.User.Email;
            user.NormalizedUserName = model.User.Email.ToUpper();

            user.WorkEmail = model.User.WorkEmail;

            await _userManager.UpdateAsync(user);
            _context.SaveChanges();

            // log user out so they can log in with new email
            await _signManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("Account/Manage/Regimental")]
        [Authorize(Roles = "Regimental")]
        public async Task<IActionResult> Regimental()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var applicationUser = _context.Users
                .Include(x => x.Regimental)
                .FirstOrDefault(x => x.Id == user.Id);

            var model = new RegimentalViewModel()
            {
                RegimentId = applicationUser.Regimental != null ? applicationUser.Regimental.RegimentId : 1,
                User = user,
                Regiments = _context.Regiments.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Regimental")]
        public IActionResult UpdateRegimental(RegimentalViewModel model)
        {
            // change regimentId for the logged in user
            var regimental = _context.Regimentals.FirstOrDefault(x => x.UserId == model.User.Id);
            regimental.RegimentId = model.RegimentId;

            _context.Regimentals.Update(regimental);
            _context.SaveChanges();

            return RedirectToAction("Regimental");
        }

        [HttpGet]
        [Route("Account/Manage/Volunteer")]
        [Authorize(Roles = "Volunteer")]
        public async Task<IActionResult> Volunteer()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var applicationUser = _context.Users
                .Include(x => x.Volunteer)
                    .ThenInclude(volunteer => volunteer.EmergencyContact)
                .FirstOrDefault(x => x.Id == user.Id);

            var model = new VolunteerViewModel()
            {
                ExpertiseId = applicationUser.Volunteer != null ? applicationUser.Volunteer.ExpertiseId : 1,
                User = user,
                Expertises = _context.Expertises.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),
                EmergencyContact = applicationUser.Volunteer.EmergencyContact != null ? applicationUser.Volunteer.EmergencyContact : new EmergencyContact()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Volunteer")]
        public IActionResult UpdateVolunteer(VolunteerViewModel model)
        {
            // change expertiseId for the logged in user
            var volunteer = _context.Volunteers.Include(x => x.EmergencyContact).FirstOrDefault(x => x.UserId == model.User.Id);
            volunteer.ExpertiseId = model.ExpertiseId;

            // update emergency contact
            volunteer.EmergencyContact.Name = model.EmergencyContact.Name;
            volunteer.EmergencyContact.Relation = model.EmergencyContact.Relation;
            volunteer.EmergencyContact.TelNo = model.EmergencyContact.TelNo;

            _context.Volunteers.Update(volunteer);
            _context.SaveChanges();

            return RedirectToAction("Volunteer");
        }

        [HttpGet]
        [Route("Account/Manage/Staff")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Staff()
        {
            var users = _context.Users.ToList();
            var roles = _context.Roles.ToList();
            var user = await _userManager.GetUserAsync(this.User);
            var applicationUser = _context.Users
                .Include(x => x.Staff)
                .ThenInclude(staff => staff.EmergencyContact)
                .FirstOrDefault(x => x.Id == user.Id);

            var model = new StaffViewModel()
            {
                User = user,
                EmergencyContact = applicationUser.Staff.EmergencyContact != null ? applicationUser.Staff.EmergencyContact : new EmergencyContact(),
                StartDate = applicationUser.Staff.StartDate,
                Users = _context.Users.Select(x => new SelectListItem()
                {
                    Text = x.UserName,
                    Value = x.Id.ToString()
                }).ToList(),
                Roles = _context.Roles.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> RemoveStaff(IFormCollection collection)
        {
            var userID = int.Parse(collection["UserID"].ToString());

            var user = _context.Staff.FirstOrDefault(x => x.UserId == userID);
            user.LeaveDate = DateTime.Now;
            user.CurrentlyEmployed = false;

            _context.Staff.Update(user);
            _context.SaveChanges();

            return RedirectToAction("Staff");
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        public IActionResult ImportUsers(StaffViewModel staffViewModel)
        {
            IFormFile file = staffViewModel.CSVFile;
            var reader = new StreamReader(file.OpenReadStream());
            int index = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var data = line.Split(new[] { ',' });
                if (index != 0)
                {
                    var decoId = int.Parse(data[3]);
                    var user = new ApplicationUser()
                    {
                        DecorationId = decoId,
                        Forename = data[1],
                        Surname = data[2],
                        AddressLine1 = data[4],
                        AddressLine2 = data[5],
                        AddressLine3 = data[6],
                        County = data[8],
                        Postcode = data[7],
                        Country = data[9],
                        PhoneNumber = data[10],
                        Email = data[0],
                        UserName = data[0],
                        IsNewsletterSubscriber = false
                    };
                    _context.Users.Add(user);
                }
                index++;
            }
            _context.SaveChanges();
            return RedirectToAction("Staff");
        }

        [HttpGet]
        [Authorize(Roles = "Staff")]
        [Route("Account/Manage/DownloadCSVTemplate")]
        public IActionResult DownloadCSVTemplate()
        {
            var csvTemplate = "wwwroot"
                + Path.DirectorySeparatorChar.ToString()
                + "Templates"
                + Path.DirectorySeparatorChar.ToString()
                + "template.csv";

            byte[] fileBytes = System.IO.File.ReadAllBytes(csvTemplate);

            return File(fileBytes, "application/force-download", "template.csv");
        }

        [HttpGet]
        [Authorize]
        [Route("Account/Manage/Invite/")]
        public async Task<IActionResult> Invite()
        {
            // get the hash code from the URL String
            var code = HttpContext.Request.Query["code"].ToString();

            //Fetch the correct email object from the EmailAuth table for comparsion
            var EmailAuth = _context.EmailAuths
                .FirstOrDefault(x => x.Hash == code);

            if (EmailAuth == null)
            {
                return View();
            }

            var roleId = EmailAuth.RoleId;
            var userId = EmailAuth.UserId;

            var user = _context.Users
                .FirstOrDefault(x => x.Id == userId);

            //make sure the role and user don't already exist
            var existingUserRole = _context.UserRoles
                .FirstOrDefault(x => x.UserId == user.Id && x.RoleId == roleId);

            _context.EmailAuths.Remove(EmailAuth);

            if (existingUserRole == null)
            {
                var model = new Object();

                var userRole = new ApplicationUserRole()
                {
                    UserId = user.Id,
                    RoleId = roleId

                };

                switch (roleId)
                {
                    case 3:
                        var StaffModel = new Staff()
                        {
                            Id = 0,
                            UserId = user.Id,
                            StartDate = System.DateTime.Now

                        };
                        _context.Staff.Add(StaffModel);

                        /*
                        model = new StaffViewModel()
                        {
                            User = user,
                            RoleId = roleId
                        };
                        */

                        break;
                    case 4:
                        var VolunteerModel = new Volunteer()
                        {
                            Id = 0,
                            UserId = user.Id
                        };
                        _context.Volunteers.Add(VolunteerModel);

                        /*
                        model = new VolunteerViewModel();
                        */

                        break;
                    case 5:
                        var DonorModel = new Donor()
                        {
                            Id = 0,
                            UserId = user.Id
                        };
                        _context.Donors.Add(DonorModel);

                        /*
                        model = new InviteViewModel()
                        {
                            User = user,
                            RoleId = roleId
                        };
                        */
                        break;
                    case 6:
                        var MemberModel = new Member()
                        {
                            Id = 0,
                            UserId = user.Id
                        };
                        _context.Members.Add(MemberModel);

                        /*
                        model = new MemberViewModel()
                        {
                            
                        };
                        */
                        break;
                    case 7:
                        var RegimentalModel = new Regimental()
                        {
                            Id = 0,
                            UserId = user.Id
                        };
                        _context.Regimentals.Add(RegimentalModel);

                        /*
                        model = new RegimentalViewModel()
                        {

                        };
                        */
                        break;
                    default:
                        model = new InviteViewModel()
                        {
                            User = user,
                            RoleId = roleId
                        };
                        break;
                }
                model = new InviteViewModel()
                {
                    User = user,
                    RoleId = roleId
                };

                _context.UserRoles.Add(userRole);
                _context.SaveChanges();
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        public IActionResult UpdateStaff(StaffViewModel model)
        {
            var staff = _context.Staff.Include(x => x.EmergencyContact).FirstOrDefault(x => x.UserId == model.User.Id);

            // update emergency contact
            staff.EmergencyContact.Name = model.EmergencyContact.Name;
            staff.EmergencyContact.Relation = model.EmergencyContact.Relation;
            staff.EmergencyContact.TelNo = model.EmergencyContact.TelNo;

            _context.Staff.Update(staff);
            _context.SaveChanges();

            return RedirectToAction("Staff");
        }

        [HttpGet]
        [Route("Account/Manage/Member")]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Member()
        {
            var user = await _userManager.GetUserAsync(this.User);
            var applicationUser = _context.Users
                .Include(x => x.Member)
                .FirstOrDefault(x => x.Id == user.Id);

            var model = new MemberViewModel()
            {
                Member = applicationUser.Member
            };

            return View(model);
        }
    }
}