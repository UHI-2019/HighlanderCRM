using System;
using System.Collections.Generic;
using System.Text;

namespace Highlander.Data.Models
{
    public class BusinessSector
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<CommercialContact> CommercialContacts { get; set; }
    }
}
