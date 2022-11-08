using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a country list.
    /// </summary>
    public partial class Country: BaseEntity
    {
        public Country()
        {
            Institutes = new HashSet<Institute>();
            States = new HashSet<State>();
            UserProfiles = new HashSet<UserProfile>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public byte CountryId { get; set; }
        /// <summary>
        /// The country name.
        /// </summary>
        public string CountryName { get; set; }
        /// <summary>
        /// If 1, the country is ready to use.
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// The date and time when this entry was last updated.
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        public virtual ICollection<Institute> Institutes { get; set; }
        public virtual ICollection<State> States { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
