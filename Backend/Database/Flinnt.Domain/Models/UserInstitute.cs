using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores user and institute mapping.
    /// </summary>
    public partial class UserInstitute: BaseEntity
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int UserInstituteId { get; set; }
        /// <summary>
        /// The user identifier belongs to the institute. Ref.: User.UserId
        /// Migrate: user_courses(find from the
        /// private course subscription)
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// The institute identifier this user belongs to. Ref.: Institute.InstituteId
        /// </summary>
        public int InstituteId { get; set; }
        /// <summary>
        /// The user type identifier this user belongs to the institute. Ref. UserType.UserTypeId
        /// </summary>
        public byte UserTypeId { get; set; }
        /// <summary>
        /// The role identifier this user belongs to the user and institute. Ref.: Role.RoleId
        /// </summary>
        public byte? RoleId { get; set; }
        /// <summary>
        /// If 1, the user and institute relation is active.
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
    }
}
