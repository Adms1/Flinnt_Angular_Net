using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a mapping between a role and permissions.
    /// </summary>
    public partial class RolePermission: BaseEntity
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int RolePermissionId { get; set; }
        /// <summary>
        /// The role identifier this permission belongs to. Ref.: Role.RoleId
        /// </summary>
        public byte RoleId { get; set; }
        /// <summary>
        /// The permission identifier this role belongs to. Ref.: Permission.PermissionId
        /// </summary>
        public short PermissionId { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual Role Role { get; set; }
    }
}
