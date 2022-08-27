﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class UserAccountHistory
    {
        public int UserAccountHistoryId { get; set; }
        public int? UserId { get; set; }
        public int? ActionUserId { get; set; }
        public string HistoryAction { get; set; }
        public DateTime? HistoryDateTime { get; set; }
        public string ClientIp { get; set; }
        public string ClientDevice { get; set; }

        public virtual User User { get; set; }
    }
}