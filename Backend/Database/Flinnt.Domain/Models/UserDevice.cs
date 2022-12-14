using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores the device information from where the user has login at least once.
    /// </summary>
    public partial class UserDevice: BaseEntity
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int UserDeviceId { get; set; }
        /// <summary>
        /// The user identifier this device belongs to. Ref.: User.UserId
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// The device type.
        /// </summary>
        public string DeviceType { get; set; }
        /// <summary>
        /// The device id. Autogenerated based on the device signature.
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// The notification identifier. Possibly the Firebase id.
        /// </summary>
        public string NotificationId { get; set; }
        /// <summary>
        /// The mobile operating system.
        /// </summary>
        public string MobileOs { get; set; }
        /// <summary>
        /// If 1, the device is ready to use.
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// If the device is mobile, then the application version.
        /// </summary>
        public string AppVersion { get; set; }
        /// <summary>
        /// The client device user agent.
        /// </summary>
        public string UserAgent { get; set; }
        /// <summary>
        /// The date and time when this device was first used.
        /// </summary>
        public DateTime? RegisterDateTime { get; set; }

        public virtual User User { get; set; }
    }
}
