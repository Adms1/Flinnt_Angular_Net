using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a list of login history.
    /// Migartion:
    /// UserId &lt; login_history.user_id
    /// LoginDateTime &lt; login_history.access_dt
    /// IsLogout &lt; login_history.is_logout
    /// ClientIP &lt; login_history.ip_addr
    /// ClientDevice &lt; login_history.device_type + login_history.device_detail
    /// AccessUrl	&lt; login_history.access_url
    /// </summary>
    public partial class LoginHistory: BaseEntity
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int LoginHistoryId { get; set; }
        /// <summary>
        /// The user identifier this history belongs to. Ref. User.UserId
        /// Migration: login_history.user_id
        /// </summary>
        public long? UserId { get; set; }
        /// <summary>
        /// The date and time when user logged in.
        /// Migration: login_history.access_dt
        /// </summary>
        public DateTime? LoginDateTime { get; set; }
        /// <summary>
        /// If 1, this entry records the log out date and time.
        /// Migration: login_history.is_logout
        /// </summary>
        public bool? IsLogout { get; set; }
        /// <summary>
        /// The client device IP address from where request has been sent.
        /// Migration: login_history.ip_addr
        /// </summary>
        public string ClientIp { get; set; }
        /// <summary>
        /// The type of client device from where request has been made.
        /// Migration: login_history.device_type + 
        /// login_history.device_detail
        /// </summary>
        public string ClientDevice { get; set; }
        /// <summary>
        /// The URL on which request has been sent.
        /// Migration: login_history.access_url
        /// </summary>
        public string AccessUrl { get; set; }

        public virtual User User { get; set; }
    }
}
