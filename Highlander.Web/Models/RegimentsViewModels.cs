using Highlander.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Highlander.Web.Models
{
    public class RegimentsIndexViewModel
    {
        public IEnumerable<Regiment> Regiments { get; set; }
    }
}
