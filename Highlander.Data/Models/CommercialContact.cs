using System;
using System.Collections.Generic;
using System.Text;

namespace Highlander.Data.Models
{
    public class CommercialContact
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public virtual IEnumerable<BusinessSector> BusinessSectors { get; set; }

    }
}
