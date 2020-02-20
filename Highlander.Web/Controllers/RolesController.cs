using System;
using System.Collections.Generic;
using System.Linq;
using Highlander.Data;
using Highlander.Data.Models;
using Highlander.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Highlander.Web.Controllers
{
    [Authorize(Roles="Superuser, Administrator")]
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Roles
        public ActionResult Index()
        {
            var model = new RolesIndexViewModel()
            {
                Roles = _context.Roles.ToList()
            };

            return View(model);
        }

        // GET: Roles/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                // Sets up a viewmodel thats we use to communicate with our view.
                var model = new RolesDetailsViewModel()
                { 
                    Role = _context.Roles
                        .Include(role => role.UserRoles)
                            .ThenInclude(userRole => userRole.User)
                        .FirstOrDefault(role => role.Id == id)
                };
                return View(model);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            var model = new ApplicationRole();
            return View(model);
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplicationRole model)
        {
            try
            {
                // new Role
                _context.Roles.Add(model);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // Move this to UserRolesController if extra behaviour is required.
        [HttpGet]
        public IActionResult AddUser(int id)
        {
            var model = new RolesAddUserViewModel()
            {
                RoleId = id,
                Role = _context.Roles.FirstOrDefault(x => x.Id == id),
                Users = _context.Users.Select(x => new SelectListItem()
                { 
                    Text = x.Email,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddUser(RolesAddUserViewModel model)
        {
            try
            {
                // New UserRole
                var userRole = new ApplicationUserRole()
                {
                    RoleId = model.RoleId,
                    UserId = model.UserId
                };

                _context.UserRoles.Add(userRole);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}