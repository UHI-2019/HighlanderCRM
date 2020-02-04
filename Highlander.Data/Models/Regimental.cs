using System;
using System.Collections.Generic;
using System.Text;

namespace Highlander.Data.Models
{
    public class Regimental
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public virtual IEnumerable<Regiment> Regiments { get; set; }
    }
}
