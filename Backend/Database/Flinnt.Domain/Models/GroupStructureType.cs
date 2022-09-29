using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores mapping between a group structure and institute types.
    /// </summary>
    public partial class GroupStructureType
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public short GroupStructureTypeId { get; set; }
        /// <summary>
        /// The group structure identifier this type belongs to.
        /// </summary>
        public byte? GroupStructureId { get; set; }
        /// <summary>
        /// The institute type identifier this group belongs to.
        /// </summary>
        public byte? InstituteTypeId { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }

        public virtual GroupStructure GroupStructure { get; set; }
        public virtual InstituteType InstituteType { get; set; }
    }
}
