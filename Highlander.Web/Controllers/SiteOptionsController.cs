using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Highlander.Data;
using Highlander.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Highlander.Web.Controllers
{
    [Authorize(Roles = "Superuser")]
    public class SiteOptionsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public SiteOptionsController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var model = _context.SiteOptions.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(IFormCollection collection)
        {
            var siteOption = new SiteOption() 
            {
                // should probably check input is safe first
                Id = int.Parse(collection["Id"]),
                Name = collection["Name"],
                Value = collection["Value"],
                Type = collection["Type"],
            };

            _context.SiteOptions.Update(siteOption);
            _context.SaveChanges();

            var model = _context.SiteOptions.ToList();
            return View(model);
        }
    }
}