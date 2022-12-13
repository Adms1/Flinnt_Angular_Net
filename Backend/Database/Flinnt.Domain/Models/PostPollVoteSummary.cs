using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores the poll summary details.
    /// Migration:
    /// PostPollId &lt; poll_result_summary.poll_id
    /// PostPollOptionId &lt; poll_result_summary.option_id
    /// VotesReceive &lt; poll_result_summary.votes_received
    /// VotePercentage &lt; poll_result_summary.votes_percent
    /// </summary>
    public partial class PostPollVoteSummary : BaseEntity
    {
        public int PostPollVoteSummaryId { get; set; }
        /// <summary>
        /// The poll identifier this summary belongs to. Ref. PostPoll.PostPollId. Migrations: poll_result_summary.poll_id
        /// </summary>
        public int PostPollId { get; set; }
        /// <summary>
        /// The poll option identifier this summary belongs to. Ref. PostPollOption.PostPollOptionId. Migrations: poll_result_summary.option_id
        /// </summary>
        public int PostPollOptionId { get; set; }
        /// <summary>
        /// The total no. of votes received. Migrations: poll_result_summary.votes_received
        /// </summary>
        public int? VotesReceive { get; set; }
        /// <summary>
        /// The percentage value of total votes received. Migrations: poll_result_summary.votes_percent
        /// </summary>
        public decimal? VotePercentage { get; set; }

        public virtual PostPoll PostPoll { get; set; }
        public virtual PostPollOption PostPollOption { get; set; }
    }
}
