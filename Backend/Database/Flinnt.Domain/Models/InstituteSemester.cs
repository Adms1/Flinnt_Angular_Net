using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity store institute and semester mapping.
    /// </summary>
    public partial class InstituteSemester: BaseEntity
    {
        public InstituteSemester()
        {
            UserInstituteGroups = new HashSet<UserInstituteGroup>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int InstituteSemesterId { get; set; }
        /// <summary>
        /// The semester identifier this institute belongs to. Ref.: Institute.InstituteId
        /// </summary>
        public byte SemesterId { get; set; }
        /// <summary>
        /// The institute identifier this semester belongs to. Ref.: Semester.SemesterId
        /// </summary>
        public int InstituteId { get; set; }
        /// <summary>
        /// The display order of this semester.
        /// </summary>
        public int? DisplayOrder { get; set; }
        /// <summary>
        /// The institute group identifier this semester belongs to.
        /// </summary>
        public int? InstituteGroupId { get; set; }
        /// <summary>
        /// The date and time when this semester starts.
        /// </summary>
        public DateTime? StartDateTime { get; set; }
        /// <summary>
        /// The date and time when this semester ends.
        /// </summary>
        public DateTime? EndDateTime { get; set; }
        /// <summary>
        /// If 1, this institute and semester mapping is ready to use.
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

        public virtual Institute Institute { get; set; }
        public virtual Semester Semester { get; set; }
        public virtual ICollection<UserInstituteGroup> UserInstituteGroups { get; set; }
    }
}
