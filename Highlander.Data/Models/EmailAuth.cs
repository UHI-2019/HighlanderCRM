using System;
using System.Collections.Generic;
using System.Text;

namespace Highlander.Data.Models
{
    public class EmailAuth
    {
        public int Id { get; set; }

        public String Hash { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }
    }
}
