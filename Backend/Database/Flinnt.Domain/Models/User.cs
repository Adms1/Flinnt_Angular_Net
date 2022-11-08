using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores the user information along with their credentials.
    /// Migrations:
    /// LoginId &lt; users.user_login
    /// RegistrationDate &lt; users.user_acc_reg_date
    /// ClientIP &lt; user_acc_history.sub_hist_user_ip
    /// ClientDevice &lt; user_acc_history.sub_hist_device_type
    /// </summary>
    public partial class User : BaseEntity
    {
        public User()
        {
            LoginHistories = new HashSet<LoginHistory>();
            Parents = new HashSet<Parent>();
            Students = new HashSet<Student>();
            UserAccountHistories = new HashSet<UserAccountHistory>();
            UserAccountVerifications = new HashSet<UserAccountVerification>();
            UserDevices = new HashSet<UserDevice>();
            UserInstituteGroups = new HashSet<UserInstituteGroup>();
            UserInstitutes = new HashSet<UserInstitute>();
            UserParentChildRelationshipChildUsers = new HashSet<UserParentChildRelationship>();
            UserParentChildRelationshipParentUsers = new HashSet<UserParentChildRelationship>();
            UserPermissions = new HashSet<UserPermission>();
            UserProfiles = new HashSet<UserProfile>();
            UserRoles = new HashSet<UserRole>();
            UserSettings = new HashSet<UserSetting>();
        }

        public long UserId { get; set; }
        /// <summary>
        /// User login id.
        /// Migration: users.user_login
        /// </summary>
        public string LoginId { get; set; }
        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Initial password which should be changed after login.
        /// </summary>
        public string OneTimePassword { get; set; }
        /// <summary>
        /// Authentication type opted by the user. Ref.: AuthenticationType.AuthTypeId
        /// </summary>
        public byte AuthenticationTypeId { get; set; }
        /// <summary>
        /// The type of the user. Ref.: UserType.UserTypeId
        /// </summary>
        public byte UserTypeId { get; set; }
        /// <summary>
        /// If 1, custom permissions are assigned to the user.
        /// </summary>
        public bool? UseCustomPermission { get; set; }
        /// <summary>
        /// The current transactional status of the users.
        /// </summary>
        public byte? StatusId { get; set; }
        /// <summary>
        /// The date and time when user account is created.
        /// Migrate: users.user_acc_reg_date
        /// </summary>
        public DateTime? RegistrationDateTime { get; set; }
        /// <summary>
        /// The Client IP address from where the request was initiated.
        /// Migrate: user_acc_history.sub_hist_user_ip
        /// </summary>
        public string ClientIp { get; set; }
        /// <summary>
        /// The type of device from where the request was initiated.
        /// Migrate: user_acc_history.sub_hist_device_type
        /// </summary>
        public string ClientDevice { get; set; }
        /// <summary>
        /// The date and time when user has last login.
        /// </summary>
        public DateTime? LastLoginDateTime { get; set; }
        /// <summary>
        /// The Client IP from where the user last login request was initiated.
        /// </summary>
        public string LastLoginIp { get; set; }
        /// <summary>
        /// The type of device from where the last login request was initiated.
        /// Migrate: user_acc_history.sub_hist_device_type
        /// </summary>
        public string LastLoginDevice { get; set; }
        /// <summary>
        /// If 1, the user account is active.
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// The reason behind deactivating the user account.
        /// </summary>
        public string InactiveReason { get; set; }
        /// <summary>
        /// If 1, the user account is deleted. This is used for soft delete.
        /// </summary>
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// The date and time when the account was deleted.
        /// </summary>
        public DateTime? DeletionDateTime { get; set; }

        public virtual AutheticationType AuthenticationType { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual ICollection<LoginHistory> LoginHistories { get; set; }
        public virtual ICollection<Parent> Parents { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<UserAccountHistory> UserAccountHistories { get; set; }
        public virtual ICollection<UserAccountVerification> UserAccountVerifications { get; set; }
        public virtual ICollection<UserDevice> UserDevices { get; set; }
        public virtual ICollection<UserInstituteGroup> UserInstituteGroups { get; set; }
        public virtual ICollection<UserInstitute> UserInstitutes { get; set; }
        public virtual ICollection<UserParentChildRelationship> UserParentChildRelationshipChildUsers { get; set; }
        public virtual ICollection<UserParentChildRelationship> UserParentChildRelationshipParentUsers { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserSetting> UserSettings { get; set; }
    }
}
