using System;
using Highlander.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Highlander.Web.Models
{
    public class RegisterAccountViewModel
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

    public class EditAccountViewModel
    {
        public ApplicationUser User { get; set; }
        public int TitleId { get; set; }
        public int DecorationId { get; set; }
        public IEnumerable<SelectListItem> Titles { get; set; }
        public IEnumerable<SelectListItem> Decorations { get; set; }
    }

    public class PhoneNumbersViewModel
    {
        public ApplicationUser User { get; set; }
    }

    public class EmailsViewModel
    {
        public ApplicationUser User { get; set; }
    }

    public class LoginViewModel
    { 
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }

    public class RegimentalViewModel
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<SelectListItem> Regiments { get; set; }
        public int RegimentId { get; set; }
    }

    public class VolunteerViewModel
    { 
        public ApplicationUser User { get; set; }
        public int ExpertiseId { get; set; }
        public IEnumerable<SelectListItem> Expertises { get; set; }
        public EmergencyContact EmergencyContact { get; set; }
    }

    public class StaffViewModel
    { 
        public ApplicationUser User { get; set; }
        public EmergencyContact EmergencyContact { get; set; }
        public DateTime StartDate { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }

    public class InviteViewModel
    {
       public int RoleId { get; set; }
       public ApplicationUser User { get; set; }
    }


    public class MemberViewModel
    { 
        public Member Member { get; set; }
        // will need to extend this in future for membership system which is why im not passing a POCO in
    }

    public class Title
    { 
        public int Id { get; set; }
        public string Value { get; set; }
    }
}