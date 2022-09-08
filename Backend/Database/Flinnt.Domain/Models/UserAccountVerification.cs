﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class UserAccountVerification : BaseEntity
    {
        public int UserAccountVerificationId { get; set; }
        public long UserId { get; set; }
        public string VerificationCode { get; set; }
        public DateTime? ExpireDateTime { get; set; }
        public bool? IsVerified { get; set; }
        public DateTime? VerifyDateTime { get; set; }
        public string VerifyClientIp { get; set; }
        public string VerifyClientDevice { get; set; }
        public DateTime? CreateDateTime { get; set; }

        public virtual User User { get; set; }
    }
}
