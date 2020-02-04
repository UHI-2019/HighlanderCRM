using System;
using System.Collections.Generic;
using System.Text;

namespace Highlander.Data.Models
{
    public class Volunteer
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public virtual IEnumerable<Expertise> Expertises { get; set; }
        public virtual IEnumerable<EmergencyContact> EmergencyContacts { get; set; }
    }
}
