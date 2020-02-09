using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Highlander.Data.Models
{
    public class ApplicationRole : IdentityRole<int>
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

    public class ApplicationUser : IdentityUser<int>
    {
        public string Title { get; set; }
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
        public string MobileTelNo { get; set; }
        public string WorkEmail { get; set; }
        public bool IsNewsletterSubscriber { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual Regimental Regimental { get; set; }
        public virtual Volunteer Volunteer { get; set; }
        public virtual Member Member { get; set; }
        public virtual Donor Donor { get; set; }
        public virtual Decoration Decoration { get; set; }
        public virtual IEnumerable<UserCommercialContact> UserCommercialContacts { get; set; }
        public virtual ICollection<IdentityUserClaim<int>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<int>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<int>> Tokens { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}