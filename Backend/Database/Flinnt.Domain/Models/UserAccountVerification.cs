using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores the user account verification requests.
    /// </summary>
    public partial class UserAccountVerification: BaseEntity
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int UserAccountVerificationId { get; set; }
        /// <summary>
        /// The user identifier this verification belongs to. Ref.: User.UserId
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// The verification code.
        /// </summary>
        public string VerificationCode { get; set; }
        /// <summary>
        /// The date and time when the code expires.
        /// </summary>
        public DateTime? ExpireDateTime { get; set; }
        /// <summary>
        /// If 1, the code is already verified.
        /// </summary>
        public bool? IsVerified { get; set; }
        /// <summary>
        /// The date and time when the code was verified.
        /// </summary>
        public DateTime? VerifyDateTime { get; set; }
        /// <summary>
        /// The Client IP address from where the verification request was initiated.
        /// </summary>
        public string VerifyClientIp { get; set; }
        /// <summary>
        /// The Client Device from where the verification request was initiated.
        /// </summary>
        public string VerifyClientDevice { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }

        public virtual User User { get; set; }
    }
}
