using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores an academic year list.
    /// </summary>
    public partial class AcademicYear: BaseEntity
    {
        public AcademicYear()
        {
            InstituteBatches = new HashSet<InstituteBatch>();
            UserInstituteGroups = new HashSet<UserInstituteGroup>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public short AcademicYearId { get; set; }
        /// <summary>
        /// The academic year display name.
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// The date when the academic year starts.
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// The date when the academic year ends.
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// If 1, the academic year is ready to use.
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
        /// <summary>
        /// The institute identifier this institute belongs to.
        /// </summary>
        public int? InstituteId { get; set; }

        public virtual Institute Institute { get; set; }
        public virtual ICollection<InstituteBatch> InstituteBatches { get; set; }
        public virtual ICollection<UserInstituteGroup> UserInstituteGroups { get; set; }
    }
}
