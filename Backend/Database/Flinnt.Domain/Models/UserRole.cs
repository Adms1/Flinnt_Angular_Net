using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a user and role mapping.
    /// </summary>
    public partial class UserRole : BaseEntity
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int UserRoleId { get; set; }
        /// <summary>
        /// The role identifier this user belongs to. Ref.: Role.RoleId
        /// </summary>
        public byte RoleId { get; set; }
        /// <summary>
        /// The user identifier this role belongs to. Ref.: User.UserId
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
