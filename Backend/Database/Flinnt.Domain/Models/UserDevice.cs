using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class UserDevice
    {
        public int UserDeviceId { get; set; }
        public int UserId { get; set; }
        public string DeviceType { get; set; }
        public string DeviceId { get; set; }
        public string NotificationId { get; set; }
        public string MobileOs { get; set; }
        public bool? IsActive { get; set; }
        public string AppVersion { get; set; }
        public string UserAgent { get; set; }
        public DateTime? RegisterDateTime { get; set; }

        public virtual User User { get; set; }
    }
}
