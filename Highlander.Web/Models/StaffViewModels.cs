﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Highlander.Web.Models
{
    public class StaffRemoveViewModel
    {
        public IEnumerable<SelectListItem> Users { get; set; }
        public int UserId { get; set; }
    }
}
