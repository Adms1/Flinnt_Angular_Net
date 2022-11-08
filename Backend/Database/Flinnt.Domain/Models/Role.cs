using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores the institute staff roles like Owner, Admin, Principal etc.
    /// </summary>
    public partial class Role: BaseEntity
    {
        public Role()
        {
            InverseParentRole = new HashSet<Role>();
            RolePermissions = new HashSet<RolePermission>();
            UserInstitutes = new HashSet<UserInstitute>();
            UserRoles = new HashSet<UserRole>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public byte RoleId { get; set; }
        /// <summary>
        /// The role name.
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// The role description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The parent role identifier this role belongs to.
        /// </summary>
        public byte? ParentRoleId { get; set; }
        /// <summary>
        /// If 1, the role is ready to use.
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

        public virtual Role ParentRole { get; set; }
        public virtual ICollection<Role> InverseParentRole { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<UserInstitute> UserInstitutes { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
