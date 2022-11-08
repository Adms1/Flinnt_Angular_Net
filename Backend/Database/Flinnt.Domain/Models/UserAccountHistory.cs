using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores the user account history.
    /// Migrations:
    /// HistoryAction &lt; user_acc_history.sub_hist_action
    /// HistoryDateTime &lt; user_acc_history.sub_hist_dt
    /// AdditionalInfo &lt; user_acc_history.sub_hist_extra
    /// ClientIp &lt; user_acc_history.sub_hist_ip
    /// ClientDevice &lt; user_acc_history.sub_hist_device_type
    /// </summary>
    public partial class UserAccountHistory : BaseEntity
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int UserAccountHistoryId { get; set; }
        /// <summary>
        /// The user identifier this history belongs to. Ref.: User.UserId
        /// </summary>
        public long? UserId { get; set; }
        /// <summary>
        /// The user identifier who has performed an action. Ref.: User.UserId
        /// </summary>
        public int? ActionUserId { get; set; }
        /// <summary>
        /// The action performed on the user account.
        /// </summary>
        public string HistoryAction { get; set; }
        /// <summary>
        /// The date and time when the action was performed.
        /// </summary>
        public DateTime? HistoryDateTime { get; set; }
        /// <summary>
        /// The Client IP address from where the request was initiated.
        /// </summary>
        public string ClientIp { get; set; }
        /// <summary>
        /// The Client device type from where the request was initiated.
        /// </summary>
        public string ClientDevice { get; set; }

        public virtual User User { get; set; }
    }
}
