using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores institute wise group details like Medium, Board and Standard.
    /// </summary>
    public partial class InstituteGroup: BaseEntity
    {
        public InstituteGroup()
        {
            InstituteDivisions = new HashSet<InstituteDivision>();
            UserInstituteGroups = new HashSet<UserInstituteGroup>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int InstituteGroupId { get; set; }
        /// <summary>
        /// The institute identifier this group belongs to. Ref.: Institute.InstituteId
        /// </summary>
        public int InstituteId { get; set; }
        /// <summary>
        /// The group structure identifier this group belongs to.
        /// </summary>
        public byte? GroupStructureId { get; set; }
        /// <summary>
        /// The board identifier this group belongs to. Ref.: Board.BoardId
        /// </summary>
        public byte? BoardId { get; set; }
        /// <summary>
        /// The medium identifier this group belongs to. Ref.: Medium.MediumId
        /// </summary>
        public byte? MediumId { get; set; }
        /// <summary>
        /// The standard identifier this group belongs to. Ref.: Standard.StandardId
        /// </summary>
        public byte? StandardId { get; set; }
        /// <summary>
        /// The display order of the group.
        /// </summary>
        public int? DisplayOrder { get; set; }
        /// <summary>
        /// If 1, the group is ready to use.
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

        public virtual Board Board { get; set; }
        public virtual GroupStructure GroupStructure { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual Medium Medium { get; set; }
        public virtual Standard Standard { get; set; }
        public virtual ICollection<InstituteDivision> InstituteDivisions { get; set; }
        public virtual ICollection<UserInstituteGroup> UserInstituteGroups { get; set; }
    }
}
