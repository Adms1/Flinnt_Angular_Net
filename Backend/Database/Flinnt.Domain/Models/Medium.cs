using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a list of language mediums.
    /// </summary>
    public partial class Medium: BaseEntity
    {
        public Medium()
        {
            InstituteConfigureSessions = new HashSet<InstituteConfigureSession>();
            InstituteGroups = new HashSet<InstituteGroup>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public byte MediumId { get; set; }
        /// <summary>
        /// The medium name.
        /// </summary>
        public string MediumName { get; set; }
        /// <summary>
        /// If 1, the medium is ready to use.
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// If 1, the medium is optional.
        /// </summary>
        public bool? IsOptional { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// The date and time when this entry was last updated.
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        public virtual ICollection<InstituteConfigureSession> InstituteConfigureSessions { get; set; }
        public virtual ICollection<InstituteGroup> InstituteGroups { get; set; }
    }
}
