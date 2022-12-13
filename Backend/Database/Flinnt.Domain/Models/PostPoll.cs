using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores poll information along with the mapped post.
    /// Migration:
    /// PostId &lt; poll.post_id
    /// EndDateTime &lt; poll.result_hours
    /// TotalVotesReceived &lt; poll.total_votes_received
    /// </summary>
    public partial class PostPoll : BaseEntity
    {
        public PostPoll()
        {
            PostPollOptions = new HashSet<PostPollOption>();
            PostPollVoteSummaries = new HashSet<PostPollVoteSummary>();
            PostPollVotes = new HashSet<PostPollVote>();
        }

        public int PostPollId { get; set; }
        /// <summary>
        /// The post identifier this poll belongs to. Ref. Post.PostId. Migrations: poll.post_id
        /// </summary>
        public int PostId { get; set; }
        /// <summary>
        /// The date and time when the poll voting ends. Migration: poll.result_hours
        /// </summary>
        public DateTime? EndDateTime { get; set; }
        /// <summary>
        /// The total no. of votes received for the poll. Migrations: poll.total_votes_received
        /// </summary>
        public int? TotalVotesReceived { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }

        public virtual Post Post { get; set; }
        public virtual ICollection<PostPollOption> PostPollOptions { get; set; }
        public virtual ICollection<PostPollVoteSummary> PostPollVoteSummaries { get; set; }
        public virtual ICollection<PostPollVote> PostPollVotes { get; set; }
    }
}
