using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a state list.
    /// </summary>
    public partial class State: BaseEntity
    {
        public State()
        {
            Cities = new HashSet<City>();
            Institutes = new HashSet<Institute>();
            UserProfiles = new HashSet<UserProfile>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int StateId { get; set; }
        /// <summary>
        /// The state name.
        /// </summary>
        public string StateName { get; set; }
        /// <summary>
        /// The country identifier this state belongs to. Ref.: Country.CountryId
        /// </summary>
        public byte CountryId { get; set; }
        /// <summary>
        /// If 1, the state is ready to use.
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

        public virtual Country Country { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Institute> Institutes { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
