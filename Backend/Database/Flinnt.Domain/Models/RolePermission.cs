using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class RolePermission
    {
        public int RolePermissionId { get; set; }
        public byte RoleId { get; set; }
        public short PermissionId { get; set; }
        public DateTime? CreateDateTime { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual Role Role { get; set; }
    }
}
