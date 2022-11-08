using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entitiy stores a list of divisions for institutes.
    /// </summary>
    public partial class InstituteDivision: BaseEntity
    {
        public InstituteDivision()
        {
            UserInstituteGroups = new HashSet<UserInstituteGroup>();
        }

        /// <summary>
        /// The unique idenfitier
        /// </summary>
        public int InstituteDivisionId { get; set; }
        /// <summary>
        /// The division name.
        /// </summary>
        public string DivisionName { get; set; }
        /// <summary>
        /// The institute group identifier this division belongs to. Ref.: InstituteGroup.InstituteGroupId
        /// </summary>
        public int InstituteGroupId { get; set; }
        /// <summary>
        /// The display order of the division.
        /// </summary>
        public int? DisplayOrder { get; set; }
        /// <summary>
        /// If 1, the division is ready to use.
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

        public virtual InstituteGroup InstituteGroup { get; set; }
        public virtual ICollection<UserInstituteGroup> UserInstituteGroups { get; set; }
    }
}
