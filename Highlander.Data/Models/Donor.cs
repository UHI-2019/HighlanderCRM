using System;
using System.Collections.Generic;
using System.Text;

namespace Highlander.Data.Models
{
    public class Donor
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual IEnumerable<DonorArtefact> DonorArtefacts { get; set; }
    }
}
