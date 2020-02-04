using Highlander.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Highlander.Web.Models
{
    public class RolesIndexViewModel
    {
        public IEnumerable<ApplicationRole> Roles { get; set; }
    }

    public class RolesDetailsViewModel
    { 
        public ApplicationRole Role { get; set; }
        public IEnumerable<ApplicationUser> Users
        { 
            get
            {
                return this.Role.UserRoles.ToList().Select(x => x.User).ToList();
            }
        }
    }

    public class RolesAddUserViewModel
    {
        // for populating the new record
        public int UserId { get; set; }
        public int RoleId { get; set; }
        
        public ApplicationRole Role { get; set; }

        // For user to select from
        public IEnumerable<SelectListItem> Users { get; set; }
    }
}
