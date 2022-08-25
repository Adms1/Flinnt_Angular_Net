using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class Semester
    {
        public Semester()
        {
            InstituteSemesters = new HashSet<InstituteSemester>();
        }

        public byte SemesterId { get; set; }
        public string SemesterName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsOptional { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual ICollection<InstituteSemester> InstituteSemesters { get; set; }
    }
}
