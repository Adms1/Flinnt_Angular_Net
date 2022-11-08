using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a city list.
    /// </summary>
    public partial class City: BaseEntity
    {
        public City()
        {
            Institutes = new HashSet<Institute>();
            UserProfiles = new HashSet<UserProfile>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// The city name.
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// The state identifier this city belongs to. Ref.: State.StateId
        /// </summary>
        public int StateId { get; set; }
        /// <summary>
        /// If 1, the city is ready to use.
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

        public virtual State State { get; set; }
        public virtual ICollection<Institute> Institutes { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
