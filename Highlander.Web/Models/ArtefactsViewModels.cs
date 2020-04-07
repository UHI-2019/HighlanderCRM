using Highlander.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Highlander.Web.Models
{
    public class ArtefactsIndexViewModel
    {
        public IEnumerable<Artefact> Artefacts { get; set; }
    }

    public class ArtefactsDetailsViewModel
    { 
        public Artefact Artefact { get; set; }
    }

    public class ArtefactsCreateViewModel
    { 
        public Artefact Artefact { get; set; }
        public int DonorId { get; set; }
        public IEnumerable<SelectListItem> Donors { get; set; }
    }
}
