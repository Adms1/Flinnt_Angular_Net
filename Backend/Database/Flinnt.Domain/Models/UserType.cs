using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a list of user types.
    /// </summary>
    public partial class UserType: BaseEntity
    {
        public UserType()
        {
            UserInstitutes = new HashSet<UserInstitute>();
            UserParentChildRelationshipChildUserTypes = new HashSet<UserParentChildRelationship>();
            UserParentChildRelationshipParentUserTypes = new HashSet<UserParentChildRelationship>();
            Users = new HashSet<User>();
        }

        /// <summary>
        /// Unique Identifier.
        /// </summary>
        public byte UserTypeId { get; set; }
        /// <summary>
        /// The user type.
        /// Possible Values: Institution Staff, Parent, Student
        /// </summary>
        public string UserType1 { get; set; }
        /// <summary>
        /// The date and time when entry was made.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// The date and time when entry was last updated.
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        public virtual ICollection<UserInstitute> UserInstitutes { get; set; }
        public virtual ICollection<UserParentChildRelationship> UserParentChildRelationshipChildUserTypes { get; set; }
        public virtual ICollection<UserParentChildRelationship> UserParentChildRelationshipParentUserTypes { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
