using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a permission list.
    /// Migrations:
    /// PermissionName &lt; permissions.perm_name
    /// Description &lt; permissions.perm_desc
    /// </summary>
    public partial class Permission: BaseEntity
    {
        public Permission()
        {
            RolePermissions = new HashSet<RolePermission>();
            UserPermissions = new HashSet<UserPermission>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public short PermissionId { get; set; }
        /// <summary>
        /// The permission name without any white space.
        /// </summary>
        public string PermissionName { get; set; }
        /// <summary>
        /// The descriptive name for the permission.
        /// </summary>
        public string PermissionTitle { get; set; }
        /// <summary>
        /// A short description on the permission.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// If 1, the permission is ready to use.
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

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }
}
