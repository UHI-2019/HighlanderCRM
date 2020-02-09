using System;
using System.Collections.Generic;
using System.Text;

namespace Highlander.Data.Models
{
    public class Volunteer
    {
        public int Id { get; set; }
        public int UserId { get; set; }        
        public int ExpertiseId { get; set; }
        public int EmergencyContactId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Expertise Expertise { get; set; }
        public virtual EmergencyContact EmergencyContact { get; set; }
    }
}