using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class UserSetting
    {
        public short UserSettingId { get; set; }
        public int UserId { get; set; }
        public bool? ReceiveUpdate { get; set; }
        public bool? ReceiveNewsletter { get; set; }
        public bool? MuteComments { get; set; }
        public bool? MuteAllNotifications { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual User User { get; set; }
    }
}
