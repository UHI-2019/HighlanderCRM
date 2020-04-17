 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Highlander.Data;
using Highlander.Data.Models;
using Highlander.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Highlander.Web.Controllers
{
    [Authorize(Roles = "Administrator, Staff, Volunteer, Donor")]
    public class ArtefactsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public ArtefactsController(ApplicationDbContext context , UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Artefacts
        public async Task<ActionResult> Index()
        {
            var model = new ArtefactsIndexViewModel();
            var user = await _userManager.GetUserAsync(this.User);
            
            // admin sees all artefacts
            if (User.IsInRole("Administrator") && (!User.IsInRole("Donor")))
            {
                // get all artefacts
                model.Artefacts = _context.Artefacts
                    .Include(x => x.DonorArtefacts)
                        .ThenInclude(donorArtefact => donorArtefact.Donor)
                            .ThenInclude(donor => donor.User)
                    .ToList();
            }
            else if (User.IsInRole("Donor"))
            {
                // get donors artefacts
                var donorId = _context.Donors.FirstOrDefault(x => x.UserId == user.Id).Id;

                model.Artefacts = _context.Artefacts
                     .Include(x => x.DonorArtefacts)
                        .ThenInclude(donorArtefact => donorArtefact.Donor)
                            .ThenInclude(donor => donor.User)
                     .Where(x => x.DonorArtefacts.Select(x => x.DonorId).Contains(donorId))
                     .ToList();
            }
            else
            {
                // get all artefacts that are not archived
                model.Artefacts = _context.Artefacts
                        .Include(x => x.DonorArtefacts)
                            .ThenInclude(donorArtefact => donorArtefact.Donor)
                                .ThenInclude(donor => donor.User)
                        .Where(x => x.IsArchived == false)
                        .ToList();
            }
            return View(model);
        }

        // GET: Artefacts/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var model = new ArtefactsDetailsViewModel()
                {
                    Artefact = _context.Artefacts
                    .Include(x => x.UserArchiveBy)
                    .Include(x => x.UserLastEditedBy)
                    .Include(x => x.DonorArtefacts)
                        .ThenInclude(donorArtefact => donorArtefact.Donor)
                            .ThenInclude(donor => donor.User)
                    .FirstOrDefault(x => x.Id == id)
                };
                return View(model);
            }
            catch
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Administrator, Staff, Volunteer")]
        public IActionResult Archive(int id)
        {
            var artefact = _context.Artefacts.FirstOrDefault(x => x.Id == id);

            artefact.IsArchived = true;

            _context.Artefacts.Update(artefact);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult UnArchive(int id)
        {
            var artefact = _context.Artefacts.FirstOrDefault(x => x.Id == id);

            artefact.IsArchived = false;

            _context.Artefacts.Update(artefact);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Artefacts/Create
        [HttpGet]
        public ActionResult Create()
        {
            var model = new ArtefactsCreateViewModel()
            {
                Artefact = new Artefact(),
                Donors = _context.Donors
                    .Include(x => x.User)
                    .Select(x => new SelectListItem()
                    { 
                        Text = x.User.Email,
                        Value = x.Id.ToString()
                    }).ToList()
            };
            return View(model);
        }

        // POST: Artefacts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ArtefactsCreateViewModel model)
        {
            try
            {
            var user = await _userManager.GetUserAsync(this.User);

            if (model.Artefact.AccessionNumber == null || model.Artefact.AdlibReference == null)
                {
                    // mark that accession must be completed
                    model.Artefact.IsCompleted = false;
                }
                else
                {
                    // complete accession
                    model.Artefact.DateAccessioned = DateTime.Now;
                    model.Artefact.IsCompleted = true;
                }
                model.Artefact.IsArchived = false;

                _context.Artefacts.Add(model.Artefact); // fails here
                _context.SaveChanges(); // fails here

                var donorArtefact = new DonorArtefact()
                {
                    ArtefactId = model.Artefact.Id // error?
                };

                // set Donor
                if (User.IsInRole("Donor")) // what if user is both a Donor and a Volunteer/Staff/Admin?
                {
                    // set currently logged in Donor
                    donorArtefact.DonorId = _context.Donors.FirstOrDefault(x => x.UserId == user.Id).Id;
                }
                else
                {
                    // set posted Donor
                    donorArtefact.DonorId = model.DonorId;
                }

                _context.DonorArtefacts.Add(donorArtefact);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Artefacts/Edit/5
        [Authorize(Roles = "Administrator, Staff, Volunteer")]
        public ActionResult Edit(int id)
        {
            var model = _context.Artefacts.FirstOrDefault(x => x.Id == id);
            return View(model);
        }

        // POST: Artefacts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Staff, Volunteer")]
        public async Task<ActionResult> Edit(Artefact model)
        {
            try
            {
                if (model.AccessionNumber != null)
                {
                    // set date
                    model.DateAccessioned = DateTime.Now;

                    // if completed correctly
                    if (model.AdlibReference != null)
                    {
                        model.IsCompleted = true;
                    }
                }

                // update last edited by
                var user = await _userManager.GetUserAsync(this.User);
                model.UserLastEditedById = user.Id;

                _context.Artefacts.Update(model);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // POST: Artefacts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            _context.Artefacts.Remove(_context.Artefacts.FirstOrDefault(x => x.Id == id));
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}