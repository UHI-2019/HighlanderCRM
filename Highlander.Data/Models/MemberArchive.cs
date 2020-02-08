using System;
using System.Collections.Generic;
using System.Text;

namespace Highlander.Data.Models
{
    public class MemberArchive
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string UserId { get; set; }
        public string Number { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public virtual Member Member { get; set; }
    }
}
