using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class UserInstitute
    {
        public int UserInstituteId { get; set; }
        public long UserId { get; set; }
        public int InstituteId { get; set; }
        public byte UserTypeId { get; set; }
        public byte RoleId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual Institute Institute { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
        public virtual UserType UserType { get; set; }
    }
}
