using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a list of institute types.
    /// </summary>
    public partial class InstituteType: BaseEntity
    {
        public InstituteType()
        {
            GroupStructureTypes = new HashSet<GroupStructureType>();
            InstituteConfigureSessions = new HashSet<InstituteConfigureSession>();
            Institutes = new HashSet<Institute>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public byte InstituteTypeId { get; set; }
        /// <summary>
        /// The type name.
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// If 1, the institute type is ready to use.
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

        public virtual ICollection<GroupStructureType> GroupStructureTypes { get; set; }
        public virtual ICollection<InstituteConfigureSession> InstituteConfigureSessions { get; set; }
        public virtual ICollection<Institute> Institutes { get; set; }
    }
}
