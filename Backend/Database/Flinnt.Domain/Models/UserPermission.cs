using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class UserPermission
    {
        public int UserPermissionId { get; set; }
        public long UserId { get; set; }
        public short PermissionId { get; set; }
        public int? InstituteId { get; set; }
        public int? CourseId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual User User { get; set; }
    }
}
