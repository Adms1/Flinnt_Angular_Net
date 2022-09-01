using System;
using System.ComponentModel.DataAnnotations;

namespace Flinnt.Business.ViewModels
{
    public class UserModel
    {
        public long UserId { get; set; }
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
    }
}