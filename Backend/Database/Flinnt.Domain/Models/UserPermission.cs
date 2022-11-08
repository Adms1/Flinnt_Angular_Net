using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a mapping between a user and permissions.
    /// </summary>
    public partial class UserPermission: BaseEntity
    {
        /// <summary>
        /// The unique identifier
        /// </summary>
        public int UserPermissionId { get; set; }
        /// <summary>
        /// The user identifier this permisssion belongs to. Ref.: User.UserId
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// The permission identifier this user belongs to. Ref.: Permission.PermissionId
        /// </summary>
        public short PermissionId { get; set; }
        /// <summary>
        /// The institute identifier this user and permission belongs to. Ref.: Institute.InstituteId
        /// </summary>
        public int? InstituteId { get; set; }
        /// <summary>
        /// The course identifier this user and permission belongs to. Ref.: Course.CourseId
        /// </summary>
        public int? CourseId { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// The date and time when this entry was last updated.
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual User User { get; set; }
    }
}
