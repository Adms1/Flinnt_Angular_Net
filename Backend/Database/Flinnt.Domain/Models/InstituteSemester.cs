using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class InstituteSemester
    {
        public int InstituteSemesterId { get; set; }
        public byte SemesterId { get; set; }
        public int InstituteId { get; set; }
        public int? DisplayOrder { get; set; }
        public int? InstituteGroupId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual Institute Institute { get; set; }
        public virtual Semester Semester { get; set; }
    }
}
