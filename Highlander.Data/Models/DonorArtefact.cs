using System;
using System.Collections.Generic;
using System.Text;

namespace Highlander.Data.Models
{
    public class DonorArtefact
    {
        public int Id { get; set; }
        public int DonorId { get; set; }
        public int ItemId { get; set; }
        public virtual Donor Donor { get; set; }
        public virtual Artefact Artefact { get; set; }
    }
}
