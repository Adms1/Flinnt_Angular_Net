using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class State : BaseEntity
    {
        public State()
        {
            Cities = new HashSet<City>();
            Institutes = new HashSet<Institute>();
            UserProfiles = new HashSet<UserProfile>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; }
        public byte CountryId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Institute> Institutes { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
