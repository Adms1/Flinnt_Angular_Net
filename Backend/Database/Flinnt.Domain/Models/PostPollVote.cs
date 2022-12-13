using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a list of votes received for a poll.
    /// Migration:
    /// PostPollId &lt; poll_result.poll_id
    /// PostPollOptionId &lt; poll_result.option_id
    /// UserId &lt; poll_result.user_id
    /// </summary>
    public partial class PostPollVote : BaseEntity
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int PostPollVote1 { get; set; }
        /// <summary>
        /// The poll identifier this vote belongs to. Ref. PostPoll.PostPollId. Migrations: poll_result.poll_id
        /// </summary>
        public int PostPollId { get; set; }
        /// <summary>
        /// The option identifier for which the user has given his vote. Ref. PostPollOption.PostPollOptionId. Migrations: poll_result.option_id
        /// </summary>
        public int PostPollOptionId { get; set; }
        /// <summary>
        /// The identifier of the user this vote belongs tol. Ref. User.UserId. Migrations: poll_result.user_id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// The date and time when the user has given the vote.
        /// </summary>
        public DateTime? VoteDateTime { get; set; }
        /// <summary>
        /// The Client IP address from where the request was initiated.
        /// </summary>
        public string ClientIp { get; set; }
        /// <summary>
        /// The Client Device from where the request was initiated.
        /// </summary>
        public string ClientDevice { get; set; }

        public virtual PostPoll PostPoll { get; set; }
        public virtual PostPollOption PostPollOption { get; set; }
        public virtual User User { get; set; }
    }
}
