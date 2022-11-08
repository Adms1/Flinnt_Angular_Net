using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores user, institute and group mapping details like standard, division etc.
    /// </summary>
    public partial class UserInstituteGroup: BaseEntity
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int UserInstituteGroupId { get; set; }
        /// <summary>
        /// The user identifier this group belongs to.
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// The institute identifier this group belongs to.
        /// </summary>
        public int InstituteId { get; set; }
        /// <summary>
        /// The institute group identifier this group belongs to.
        /// </summary>
        public int? InstituteGroupId { get; set; }
        /// <summary>
        /// The institute division identifier this group belongs to.
        /// </summary>
        public int? InstituteDivisionId { get; set; }
        /// <summary>
        /// The institute semester identifier this group belongs to.
        /// </summary>
        public int? InstituteSemesterId { get; set; }
        /// <summary>
        /// The academic year identifier this group belongs to.
        /// </summary>
        public short? AcademicYearId { get; set; }
        /// <summary>
        /// The institute batch identifier this group belongs to.
        /// </summary>
        public int? InstituteBatchId { get; set; }
        /// <summary>
        /// The institute session identifier this group belongs to.
        /// </summary>
        public int? InstituteSessionId { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual InstituteBatch InstituteBatch { get; set; }
        public virtual InstituteDivision InstituteDivision { get; set; }
        public virtual InstituteGroup InstituteGroup { get; set; }
        public virtual InstituteSemester InstituteSemester { get; set; }
        public virtual InstituteSession InstituteSession { get; set; }
        public virtual User User { get; set; }
    }
}
