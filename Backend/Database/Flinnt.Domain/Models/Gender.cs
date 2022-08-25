using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class Gender
    {
        public Gender()
        {
            UserProfiles = new HashSet<UserProfile>();
        }

        public byte GenderId { get; set; }
        public string Gender1 { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
