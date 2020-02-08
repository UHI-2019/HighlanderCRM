using System;
using System.Collections.Generic;
using System.Text;

namespace Highlander.Data.Models
{
    public class UserCommercialContact
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CommercialContactId { get; set; }
        public virtual CommercialContact CommercialContact { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
