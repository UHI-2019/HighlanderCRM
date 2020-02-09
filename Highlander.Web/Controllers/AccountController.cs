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
            var model = new RegisterViewModel()
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
        public async Task<IActionResult> Register(RegisterViewModel model)
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
                    }
                }
            }
            return View();
        }
    }
}