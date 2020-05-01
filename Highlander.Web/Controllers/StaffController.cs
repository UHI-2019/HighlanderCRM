using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Highlander.Data;
using Highlander.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Highlander.Web.Controllers
{
    public class StaffController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public StaffController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public ActionResult Remove()
        {
            var model = new StaffRemoveViewModel()
            {
                Users = _context.ApplicationUsers
                    .Where(x => x.Staff != null)
                    .Select(x => new SelectListItem()
                    {
                        Text = x.Email,
                        Value = x.Id.ToString()
                    })
                    .ToList(),
            };
            return View(model);
        }

        public ActionResult RemoveStaff(StaffRemoveViewModel model)
        {
            var userId = model.UserId;

            var user = _context.Staff.FirstOrDefault(x => x.UserId == userId);
            user.LeaveDate = DateTime.Now;
            user.CurrentlyEmployed = false;

            _context.Staff.Update(user);
            _context.SaveChanges();

            return RedirectToAction("Remove");
        }
    }
}