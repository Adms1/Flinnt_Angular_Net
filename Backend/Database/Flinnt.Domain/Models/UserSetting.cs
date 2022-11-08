using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores user settings.
    /// Migrations:
    /// ReceiveUpdate &lt; users.user_receive_update
    /// ReceiveNewsletter &lt; users.user_receive_newsletter
    /// 
    /// </summary>
    public partial class UserSetting : BaseEntity
    {
        /// <summary>
        /// Unique Identifier.
        /// </summary>
        public short UserSettingId { get; set; }
        /// <summary>
        /// The user identifier these settings belongs to. Ref.: User.UserId
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// If 1, user will receive update from the platform.
        /// Migrate: users.user_receive_update
        /// </summary>
        public bool? ReceiveUpdate { get; set; }
        /// <summary>
        /// If 1, user will receive newsletters from the platform.
        /// Migrate: users.user_receive_newsletter
        /// </summary>
        public bool? ReceiveNewsletter { get; set; }
        /// <summary>
        /// If 1, comments notification sound will be muted.
        /// </summary>
        public bool? MuteComments { get; set; }
        /// <summary>
        /// If 1, all notifications sound will be muted.
        /// </summary>
        public bool? MuteAllNotifications { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// The date and time when these settings were last updated.
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        public virtual User User { get; set; }
    }
}
