using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class UserType
    {
        public UserType()
        {
            UserInstitutes = new HashSet<UserInstitute>();
            Users = new HashSet<User>();
        }

        public byte UserTypeId { get; set; }
        public string UserType1 { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual ICollection<UserInstitute> UserInstitutes { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
