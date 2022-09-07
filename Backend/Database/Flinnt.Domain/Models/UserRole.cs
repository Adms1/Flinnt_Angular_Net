using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class UserRole
    {
        public int UserRoleId { get; set; }
        public byte RoleId { get; set; }
        public long UserId { get; set; }
        public DateTime? CreateDateTime { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
