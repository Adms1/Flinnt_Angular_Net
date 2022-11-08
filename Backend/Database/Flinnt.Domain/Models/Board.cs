using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a list of education boards.
    /// </summary>
    public partial class Board: BaseEntity
    {
        public Board()
        {
            InstituteConfigureSessions = new HashSet<InstituteConfigureSession>();
            InstituteGroups = new HashSet<InstituteGroup>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public byte BoardId { get; set; }
        /// <summary>
        /// The education board name.
        /// </summary>
        public string BoardName { get; set; }
        /// <summary>
        /// The education board short name.
        /// </summary>
        public string BoardShortName { get; set; }
        /// <summary>
        /// If 1, the education board is ready to use.
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// If 1, the board is optional.
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
