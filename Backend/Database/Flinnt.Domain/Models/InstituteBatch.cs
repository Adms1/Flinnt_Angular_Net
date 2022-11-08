using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a list of batches for institutes.
    /// </summary>
    public partial class InstituteBatch: BaseEntity
    {
        public InstituteBatch()
        {
            InstituteSessions = new HashSet<InstituteSession>();
            UserInstituteGroups = new HashSet<UserInstituteGroup>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int InstituteBatchId { get; set; }
        /// <summary>
        /// The academic year this batch belongs to. Ref.: AcademicYear.YearId
        /// </summary>
        public short? AcademicYearId { get; set; }
        /// <summary>
        /// The batch name.
        /// </summary>
        public string BatchName { get; set; }
        /// <summary>
        /// The time when the batch starts.
        /// </summary>
        public TimeSpan? StartTime { get; set; }
        /// <summary>
        /// The time when the batch ends.
        /// </summary>
        public TimeSpan? EndTime { get; set; }
        /// <summary>
        /// If 1, the batch is ready to use.
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

        public virtual AcademicYear AcademicYear { get; set; }
        public virtual ICollection<InstituteSession> InstituteSessions { get; set; }
        public virtual ICollection<UserInstituteGroup> UserInstituteGroups { get; set; }
    }
}
