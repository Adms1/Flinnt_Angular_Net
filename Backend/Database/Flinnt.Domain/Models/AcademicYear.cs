using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class AcademicYear
    {
        public AcademicYear()
        {
            InstituteBatches = new HashSet<InstituteBatch>();
        }

        public short AcademicYearId { get; set; }
        public string DisplayName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual ICollection<InstituteBatch> InstituteBatches { get; set; }
    }
}
