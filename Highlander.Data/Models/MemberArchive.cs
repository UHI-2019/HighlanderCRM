﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Highlander.Data.Models
{
    class MemberArchive
    {
        public int Id { get; set; }
        public virtual IEnumerable<Member> Members { get; set; }
        public string UserId { get; set; }
        public string Number { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
