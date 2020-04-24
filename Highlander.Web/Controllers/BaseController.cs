using Highlander.Data;
using Highlander.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Highlander.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly ApplicationDbContext _context;
        public string SiteTitle { get; set; }
        public string SiteColour { get; set; }

        public BaseController(ApplicationDbContext context)
        {
            _context = context;

            var siteTitleOption = _context.SiteOptions.FirstOrDefault(x => x.Name == "site-name");
            var siteColourOption = _context.SiteOptions.FirstOrDefault(x => x.Name == "primary-colour");
            SiteTitle = siteTitleOption.Value;
            SiteColour = siteColourOption.Value;

            _context.Entry<SiteOption>(siteTitleOption).State = EntityState.Detached;
            _context.Entry<SiteOption>(siteColourOption).State = EntityState.Detached;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.SiteTitle = SiteTitle;
            ViewBag.SiteColour = SiteColour;
        }
    }
}
