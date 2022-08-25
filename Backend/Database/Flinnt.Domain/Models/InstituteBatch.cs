using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class InstituteBatch
    {
        public InstituteBatch()
        {
            InstituteSessions = new HashSet<InstituteSession>();
        }

        public int InstituteBatchId { get; set; }
        public int InstituteId { get; set; }
        public short? AcademicYearId { get; set; }
        public string BatchName { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual ICollection<InstituteSession> InstituteSessions { get; set; }
    }
}
