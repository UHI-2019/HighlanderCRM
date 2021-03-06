﻿using Highlander.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

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
        public IFormFile Image { get; set; }
        public int DonorId { get; set; }
        public IEnumerable<SelectListItem> Donors { get; set; }
    }
}
