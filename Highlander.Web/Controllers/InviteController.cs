using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Highlander.Data;
using Highlander.Data.Models;
using Highlander.Web.Helpers;
using Highlander.Web.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit;

namespace Highlander.Web.Controllers
{
    [Authorize(Roles = "Superuser, Administrator")]
    public class InviteController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public InviteController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            var model = new InviteIndexViewModel()
            {
                Users = _context.Users.Select(x => new SelectListItem()
                {
                    Text = x.UserName,
                    Value = x.Id.ToString()
                }).ToList(),
                Roles = _context.Roles.Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View(model);
        }

        public async Task<IActionResult> InviteUserToRole(IFormCollection collection)
        {
            var rawRole = collection["RoleId"].ToString();

            var roleId = int.Parse(rawRole);

            EmailHelper helper = new EmailHelper();

            var user = await _userManager.FindByIdAsync(collection["UserId"]);
            var hashCode = helper.RandomString(32);

            var Role = _context.Roles
              .FirstOrDefault(x => x.Id == roleId);

            var emailAuth = new EmailAuth()
            {
                Id = 0,
                Hash = hashCode,
                RoleId = Role.Id,
                UserId = user.Id
            };

            _context.EmailAuths.Add(emailAuth);
            _context.SaveChanges();

            var connectionURL = "smtp.gmail.com";
            var emailUsername = "mralimac@googlemail.com";
            var emailPassword = "vlntgyrchikfkhvb";
            var hostname = "https://localhost:44390";

            var emailTemplate = "wwwroot"
                + Path.DirectorySeparatorChar.ToString()
                + "Templates"
                + Path.DirectorySeparatorChar.ToString()
                + "InviteByEmailTemplate.html";

            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("Highlander Museum", "noreply@thehighlandersmuseum.com");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress(user.Forename + " " + user.Surname, user.Email);
            message.To.Add(to);

            message.Subject = "You have been invited to a new role!";

            var linkURL = hostname + "/Account/Manage/Invite?code=" + hashCode;

            BodyBuilder bodyBuilder = new BodyBuilder();

            using (StreamReader SourceReader = System.IO.File.OpenText(emailTemplate))
            {
                bodyBuilder.HtmlBody = string.Format(SourceReader.ReadToEnd(),
                    user.Forename,
                    user.Surname,
                    Role.Name,
                    linkURL,
                    linkURL
                  );
            }

            message.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            client.Connect(connectionURL, 25, false);
            client.Authenticate(emailUsername, emailPassword);
            //client.Authenticate("insert gmail email", "insert gmail password");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();

            return RedirectToAction("Index");
        }
    }
}