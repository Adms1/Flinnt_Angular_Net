using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class User : BaseEntity
    {
        public User()
        {
            UserAccountHistories = new HashSet<UserAccountHistory>();
            UserAccountVerifications = new HashSet<UserAccountVerification>();
            UserDevices = new HashSet<UserDevice>();
            UserInstitutes = new HashSet<UserInstitute>();
            UserPermissions = new HashSet<UserPermission>();
            UserProfiles = new HashSet<UserProfile>();
            UserRoles = new HashSet<UserRole>();
            UserSettings = new HashSet<UserSetting>();
        }

        public int UserId { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string OneTimePassword { get; set; }
        public byte AuthenticationTypeId { get; set; }
        public byte UserTypeId { get; set; }
        public bool? UseCustomPermission { get; set; }
        public byte? StatusId { get; set; }
        public DateTime? RegistrationDateTime { get; set; }
        public string ClientIp { get; set; }
        public string ClientDevice { get; set; }
        public DateTime? LastLoginDateTime { get; set; }
        public string LastLoginIp { get; set; }
        public string LastLoginDevice { get; set; }
        public bool? IsActive { get; set; }
        public string InactiveReason { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? DeletionDateTime { get; set; }

        public virtual AutheticationType AuthenticationType { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual ICollection<UserAccountHistory> UserAccountHistories { get; set; }
        public virtual ICollection<UserAccountVerification> UserAccountVerifications { get; set; }
        public virtual ICollection<UserDevice> UserDevices { get; set; }
        public virtual ICollection<UserInstitute> UserInstitutes { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserSetting> UserSettings { get; set; }
    }
}
