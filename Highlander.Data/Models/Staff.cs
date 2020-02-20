using System;
using System.Collections.Generic;
using System.Text;

namespace Highlander.Data.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EmergencyContactId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? LeaveDate { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual EmergencyContact EmergencyContact { get; set; }
    }
}
