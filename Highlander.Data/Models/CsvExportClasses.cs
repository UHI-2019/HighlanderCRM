using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Highlander.Web.Models
{
    public class NewsletterSubscriber
    {
        public string Email { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
    }

    public class PersonalData
    { 
        public string Title { get; set; }
        public string Forename { get; set; }
        public string Initial { get; set; }
        public string Surname { get; set; }
        public string Decoration { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileTelNo { get; set; }
        public string Email { get; set; }
        public string WorkEmail { get; set; }
        public string SubscribedToNewsLetter { get; set; }
        public string MemberType { get; set; }
        public string MemberNumber { get; set; }
        public DateTime? MemberStartDate { get; set; }
        public DateTime? MemberExpiryDate { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactRelation { get; set; }
        public string EmergencyContactTelNo { get; set; }
        public DateTime? StartOfEmployment { get; set; }
        public DateTime? EndOfEmployment { get; set; }
        public string Regiment { get; set; }
        public string Expertise { get; set; }
    }
}
