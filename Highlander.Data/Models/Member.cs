using System;
using System.Collections.Generic;
using System.Text;

namespace Highlander.Data.Models
{
    public class Member
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public virtual IEnumerable<MemberArchive> MembersArchives { get; set; }
    }
}
