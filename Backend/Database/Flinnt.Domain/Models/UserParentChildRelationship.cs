using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores information regarding a parent and child relationship between two users.
    /// </summary>
    public partial class UserParentChildRelationship: BaseEntity
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int UserParentChildRelationshipId { get; set; }
        /// <summary>
        /// The parent user identifier this relationship belongs to.
        /// </summary>
        public long ParentUserId { get; set; }
        /// <summary>
        /// The parent user type identifier this relationship belongs to.
        /// </summary>
        public byte ParentUserTypeId { get; set; }
        /// <summary>
        /// The child user identifier this relationship belongs to.
        /// </summary>
        public long ChildUserId { get; set; }
        /// <summary>
        /// The child user type identifier this relationship belongs to.
        /// </summary>
        public byte ChildUserTypeId { get; set; }
        /// <summary>
        /// The institute identifier this relationship belongs to.
        /// </summary>
        public int InstituteId { get; set; }
        /// <summary>
        /// If 1, the relationship is intact.
        /// </summary>
        public byte? IsActive { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// The date and time when this entry was last updated.
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        public virtual User ChildUser { get; set; }
        public virtual UserType ChildUserType { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual User ParentUser { get; set; }
        public virtual UserType ParentUserType { get; set; }
    }
}
