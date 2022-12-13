using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores log of actions performed on a post.
    /// </summary>
    public partial class PostLog : BaseEntity
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public long PostLogId { get; set; }
        /// <summary>
        /// The post identifier this log belongs to. Ref. Post.PostId
        /// </summary>
        public int PostId { get; set; }
        /// <summary>
        /// The user identifier this log belongs to. Ref. User.UserId
        /// </summary>
        public long? UserId { get; set; }
        /// <summary>
        /// The type of action performed.
        /// </summary>
        public string ActionType { get; set; }
        /// <summary>
        /// The date and time when this action was performed.
        /// </summary>
        public DateTime? ActionDateTime { get; set; }
        /// <summary>
        /// Any additional information to store for the action performed. Store data in the JSON format.
        /// </summary>
        public string ExtraInformation { get; set; }
        /// <summary>
        /// The Client IP address from where the request was initiated.
        /// </summary>
        public string ClientIp { get; set; }
        /// <summary>
        /// The Client Device from where the request was initiated.
        /// </summary>
        public string ClientDevice { get; set; }
    }
}
