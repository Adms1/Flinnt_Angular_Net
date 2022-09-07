using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class City : BaseEntity
    {
        public City()
        {
            Institutes = new HashSet<Institute>();
            UserProfiles = new HashSet<UserProfile>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual State State { get; set; }
        public virtual ICollection<Institute> Institutes { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
