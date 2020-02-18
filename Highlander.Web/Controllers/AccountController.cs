using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Highlander.Data;
using Highlander.Data.Models;
using Highlander.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            model.TitleId = model.TitleId; // this is wrong - titleId isnt being set

            return View(model);
        }

        [HttpPost]
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
        public async Task<IActionResult> PhoneNumbers()
        {
            var model = new PhoneNumbersViewModel()
            {
                User = await _userManager.GetUserAsync(this.User)
            };
            return View(model);
        }

        [HttpPost]
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
        [Route("Account/Manage/PersonalData")]
        public async Task<IActionResult> PersonalData()
        {
            var model = new PhoneNumbersViewModel()
            {
                User = await _userManager.GetUserAsync(this.User)
            };

            // let user download personal data as csv

            return View();
        }

        [HttpGet]
        [Route("Account/Manage/Emails")]
        public async Task<IActionResult> Emails()
        {
            var model = new EmailsViewModel()
            {
                User = await _userManager.GetUserAsync(this.User)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> StoreEmails(EmailsViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.User.Id.ToString());

            user.Email = model.User.Email;
            user.NormalizedEmail = model.User.Email.ToUpper();

            // also update username
            user.UserName = model.User.Email;
            user.NormalizedUserName = model.User.Email.ToUpper();

            user.WorkEmail = model.User.WorkEmail;

            await _userManager.UpdateAsync(user);
            _context.SaveChanges();

            // log user out
            await _signManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}