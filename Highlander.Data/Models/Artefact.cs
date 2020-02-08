using System;
using System.Collections.Generic;
using System.Text;

namespace Highlander.Data.Models
{
    public class Artefact
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string AccessionNumber { get; set; }
        public DateTime DateAccessioned { get; set; }
        public string AdlibReference { get; set; }

        // can an Artefact have mutliple donors?
        public virtual IEnumerable<DonorArtefact> DonorArtefacts { get; set; }
    }
}