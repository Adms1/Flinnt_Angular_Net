using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores an authentication type list.
    /// </summary>
    public partial class AutheticationType: BaseEntity
    {
        public AutheticationType()
        {
            Users = new HashSet<User>();
        }

        /// <summary>
        /// Unique Identifier.
        /// </summary>
        public byte AuthenticationTypeId { get; set; }
        /// <summary>
        /// The authentication type.
        /// </summary>
        public string AuthenticationType { get; set; }
        /// <summary>
        /// The date and time when entry was made.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// The date and time when entry was last updated.
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
