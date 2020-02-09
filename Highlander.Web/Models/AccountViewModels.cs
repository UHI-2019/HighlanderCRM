using Highlander.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Highlander.Web.Models
{
    public class RegisterViewModel
    {
        public int TitleId { get; set; }
        public string Forename { get; set; }
        public string Initial { get; set; }
        public string Surname { get; set; }
        public int DecorationId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsNewsletterSubscriber { get; set; }

        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public IEnumerable<SelectListItem> Titles { get; set; }
        public IEnumerable<SelectListItem> Decorations { get; set; }
    }

    public class Title
    { 
        public int Id { get; set; }
        public string Value { get; set; }
    }
}