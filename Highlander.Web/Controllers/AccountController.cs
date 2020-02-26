using CsvHelper;
using Highlander.Data;
using Highlander.Data.Models;
using Highlander.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace Highlander.Web.Controllers
{
    public class AccountController : Controller
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

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager, ApplicationDbContext context)
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
                     // fails here cause there not being created
                {
                    await _signManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
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
            var result = await _signManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                if (model.ReturnUrl != null)
                {
                    return Redirect(model.ReturnUrl);
                }
            }
            else if (result.IsLockedOut)
            { 
                // add recovery logic
            }
            return RedirectToAction("Index", "Home");
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
        public async Task<IActionResult> PersonalData()
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

            // dynamically create a user to be downloaded as a CSV
            dynamic displayUser = new ExpandoObject();

            displayUser.Title = applicationUser.Title;
            displayUser.Forename = applicationUser.Forename;
            displayUser.Initial = applicationUser.Initial;
            displayUser.Surname = applicationUser.Surname;
            displayUser.Decoration = applicationUser.Decoration.Name;
            displayUser.AddressLine1 = applicationUser.AddressLine1;
            displayUser.AddressLine2 = applicationUser.AddressLine2;
            displayUser.AddressLine3 = applicationUser.AddressLine3;
            displayUser.County = applicationUser.County;
            displayUser.Country = applicationUser.Country;
            displayUser.Postcode = applicationUser.Postcode;
            displayUser.PhoneNumber = applicationUser.PhoneNumber;
            displayUser.MobileTelNo = applicationUser.MobileTelNo;
            displayUser.Email = applicationUser.Email;
            displayUser.WorkEmail = applicationUser.WorkEmail;
            displayUser.SubscribedToNewsLetter = applicationUser.IsNewsletterSubscriber ? "Yes" : "No";
            
            if (applicationUser.Member != null)
            {
                displayUser.MemberType = applicationUser.Member.Type;
                displayUser.MemberNumber = applicationUser.Member.Number;
                displayUser.MemberStartDate = applicationUser.Member.StartDate;
                displayUser.MemberExpiryDate = applicationUser.Member.ExpiryDate;
            }

            if (applicationUser.Staff != null)
            {
                displayUser.EmergencyContactName = applicationUser.Staff.EmergencyContact.Name;
                displayUser.EmergencyContactRelation = applicationUser.Staff.EmergencyContact.Relation;
                displayUser.EmergencyContactTelNo = applicationUser.Staff.EmergencyContact.TelNo;
                displayUser.StartOfEmployment = applicationUser.Staff.StartDate;
                displayUser.EndOfEmployment = applicationUser.Staff.LeaveDate;
            }

            if (applicationUser.Regimental != null)
            {
                displayUser.Regiment = applicationUser.Regimental.Regiment.Name;
            }

            if (applicationUser.Volunteer != null)
            {
                // if they are staff this will overwrite the already set EmergencyContact so it wont be repeated
                // might be worth moving emergency contact from both Staff and Volunteer and having it in User
                displayUser.EmergencyContactName = applicationUser.Volunteer.EmergencyContact.Name;
                displayUser.EmergencyContactRelation = applicationUser.Volunteer.EmergencyContact.Relation;
                displayUser.EmergencyContactTelNo = applicationUser.Volunteer.EmergencyContact.TelNo;

                displayUser.Expertise = applicationUser.Volunteer.Expertise.Name;
            }

            // return a csv file of the current users 'user' record and any extra roles
            var result = this.WriteCsvToMemory(displayUser);
            var memoryStream = new MemoryStream(result);
            return new FileStreamResult(memoryStream, "text/csv") { FileDownloadName = "export.csv" };
        }

        /**
         * Move to own helper class? If we need to use it again
         */
        public byte[] WriteCsvToMemory(dynamic record) 
        {
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {

                var records = new List<dynamic>();
                records.Add(record);

                csvWriter.WriteRecords(records);

                streamWriter.Flush();
                return memoryStream.ToArray();
            }
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

        [HttpGet]
        [Authorize]
        [Route("Account/Manage/Invite/")]
        public async Task<IActionResult> Invite()
        {
            var rawRole = HttpContext.Request.Query["roleId"].ToString();
            var roleId = int.Parse(rawRole);
            var user = await _userManager.GetUserAsync(this.User);

            var model = new InviteViewModel()
            {
                User = user,
                RoleId = roleId

            };

            var userRole = new ApplicationUserRole()
            {
                UserId = user.Id,
                RoleId = roleId
            };


            //Not sure how to insert into database, send help
                                 
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        public IActionResult InviteUserToRole(StaffViewModel model)
        {
            var staff = _context.Staff.FirstOrDefault(x => x.UserId == model.User.Id);
           


            var roleId = "null";

            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("Admin","admin@admin.com");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress(model.User.Forename + " " + model.User.Surname, model.User.Email);
            message.To.Add(to);

            message.Subject = "You have been invited to a new role!";

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = "<h1>Hello World!</h1>";
            bodyBuilder.TextBody = "Please click the following link to gain access " + baseURL + ";

            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 465, true);
            
            client.Authenticate("insert gmail email", "insert gmail password");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();

            return RedirectToAction("Staff");
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