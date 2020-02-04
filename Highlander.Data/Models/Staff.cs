using System;
using System.Collections.Generic;
using System.Text;

namespace Highlander.Data.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual IEnumerable<EmergencyContact> EmergencyContacts { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LeaveDate { get; set; }

    }
}
