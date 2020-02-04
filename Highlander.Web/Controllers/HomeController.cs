using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Highlander.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Highlander.Data;
using Microsoft.EntityFrameworkCore;

namespace Highlander.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Volunteer")]
        public IActionResult Privacy()
        {
            var user = _context.Users
                .Include(user => user.UserRoles)
                    .ThenInclude(userRole => userRole.Role)
                .FirstOrDefault();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
