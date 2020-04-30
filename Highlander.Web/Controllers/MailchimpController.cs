using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Highlander.Data;
using Highlander.Web.Helpers;
using Highlander.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Highlander.Web.Controllers
{
    [Authorize(Roles = "Superuser, Administrator")]
    public class MailchimpController
    {
        private readonly ApplicationDbContext _context;

        public MailchimpController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public FileStreamResult Export()
        {
            List<NewsletterSubscriber> newsLettersubscribers = _context.Users
                .Where(x => x.IsNewsletterSubscriber == true)
                .Select(x => new NewsletterSubscriber()
                {
                    Email = x.Email,
                    Forename = x.Forename,
                    Surname = x.Surname
                }).ToList();

            var result = CsvUtilities.WriteCsvToMemory(newsLettersubscribers);
            var memoryStream = new MemoryStream(result);
            return new FileStreamResult(memoryStream, "text/csv") { FileDownloadName = "mailchimpexport.csv" };
        }
    }
}