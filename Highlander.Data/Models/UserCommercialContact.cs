using System;
using System.Collections.Generic;
using System.Text;

namespace Highlander.Data.Models
{
    public class UserCommercialContact
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual IEnumerable<CommercialContact> CommercialContacts { get; set; }
    }
}
