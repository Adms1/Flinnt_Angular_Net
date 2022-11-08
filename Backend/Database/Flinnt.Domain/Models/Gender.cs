using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a gender list.
    /// </summary>
    public partial class Gender: BaseEntity
    {
        public Gender()
        {
            Students = new HashSet<Student>();
            UserProfiles = new HashSet<UserProfile>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public byte GenderId { get; set; }
        /// <summary>
        /// The gender.
        /// </summary>
        public string Gender1 { get; set; }
        /// <summary>
        /// If 1, the gender is ready to use.
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

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
