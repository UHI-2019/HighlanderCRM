using System;
using System.Collections.Generic;
using System.Text;

namespace Highlander.Data.Models
{
    public class Regiment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Regimental> Regimentals { get; set; }
    }
}