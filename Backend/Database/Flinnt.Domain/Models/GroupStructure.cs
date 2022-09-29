using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a list of group structures, like: Board-&gt;Medium-&gt;Standard, Board-&gt;Medium-&gt;Standard-&gt;Division etc.
    /// </summary>
    public partial class GroupStructure : BaseEntity
    {
        public GroupStructure()
        {
            GroupStructureTypes = new HashSet<GroupStructureType>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public byte GroupStructureId { get; set; }
        /// <summary>
        /// The group structure title.
        /// </summary>
        public string StructureTitle { get; set; }
        /// <summary>
        /// The group structure description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The display order of the group structure.
        /// </summary>
        public byte? DisplayOrder { get; set; }
        /// <summary>
        /// If 1, the group structure is ready to use.
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
    }
}
