using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class Country : BaseEntity
    {
        public Country()
        {
            Institutes = new HashSet<Institute>();
            States = new HashSet<State>();
            UserProfiles = new HashSet<UserProfile>();
        }

        public byte CountryId { get; set; }
        public string CountryName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual ICollection<Institute> Institutes { get; set; }
        public virtual ICollection<State> States { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
