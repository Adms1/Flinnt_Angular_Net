using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a standard list.
    /// </summary>
    public partial class Standard: BaseEntity
    {
        public Standard()
        {
            InstituteGroups = new HashSet<InstituteGroup>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public byte StandardId { get; set; }
        /// <summary>
        /// The standard name.
        /// </summary>
        public string StandardName { get; set; }
        /// <summary>
        /// If 1, the standard is ready to use.
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// If 1, the standard is optional.
        /// </summary>
        public bool? IsOptional { get; set; }
        /// <summary>
        /// The display order of the standard.
        /// </summary>
        public byte? DisplayOrder { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// The date and time when this entry was last updated.
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        public virtual ICollection<InstituteGroup> InstituteGroups { get; set; }
    }
}
