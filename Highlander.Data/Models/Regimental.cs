using System;
using System.Collections.Generic;
using System.Text;

namespace Highlander.Data.Models
{
    public class Regimental
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RegimentId { get; set; }
        public virtual IEnumerable<Regiment> Regiments { get; set; }
    }
}
