using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a semester list.
    /// </summary>
    public partial class Semester: BaseEntity
    {
        public Semester()
        {
            InstituteSemesters = new HashSet<InstituteSemester>();
        }

        /// <summary>
        /// The unique idenfier.
        /// </summary>
        public byte SemesterId { get; set; }
        /// <summary>
        /// The semester name.
        /// </summary>
        public string SemesterName { get; set; }
        /// <summary>
        /// If 1, the semester is ready to use.
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// If 1, the semester is optional.
        /// </summary>
        public bool? IsOptional { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// The date and time when this entry was last updated.
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        public virtual ICollection<InstituteSemester> InstituteSemesters { get; set; }
    }
}
