using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class AutheticationType
    {
        public AutheticationType()
        {
            Users = new HashSet<User>();
        }

        public byte AuthenticationTypeId { get; set; }
        public string AuthenticationType { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
