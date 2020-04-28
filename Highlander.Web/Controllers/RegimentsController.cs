using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Highlander.Data;
using Highlander.Data.Models;
using Highlander.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace Highlander.Web.Controllers
{
    [Authorize(Roles = "Administrator, Staff")]
    public class RegimentsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public RegimentsController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new RegimentsIndexViewModel()
            {
                Regiments = _context.Regiments.ToList()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new Regiment();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Regiment model)
        {
            _context.Regiments.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var model = _context.Regiments.FirstOrDefault(x => x.Id == id);
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _context.Regiments.FirstOrDefault(x => x.Id == id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Regiment model)
        {
            _context.Regiments.Update(model);
            _context.SaveChanges();

            return RedirectToAction("Details", new { Id = model.Id });
        }

        public IActionResult Delete(int id)
        {
            var model = _context.Regiments.FirstOrDefault(x => x.Id == id);

            _context.Regiments.Remove(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}