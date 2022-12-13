using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a list of poll options.
    /// Migration:
    /// PostPollId &lt; poll_options.poll_id
    /// OptionText &lt; poll_options.option_text
    /// </summary>
    public partial class PostPollOption : BaseEntity
    {
        public PostPollOption()
        {
            PostPollVoteSummaries = new HashSet<PostPollVoteSummary>();
            PostPollVotes = new HashSet<PostPollVote>();
        }

        public int PostPollOptionId { get; set; }
        /// <summary>
        /// The poll identifier this option belongs to. Ref. PostPoll.PostPollId. Migrations: poll_options.poll_id
        /// </summary>
        public int PostPollId { get; set; }
        /// <summary>
        /// The poll option text. Migrations: poll_options.option_text
        /// </summary>
        public string OptionText { get; set; }
        /// <summary>
        /// The display order of the poll option.
        /// </summary>
        public int? DisplayOrder { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }

        public virtual PostPoll PostPoll { get; set; }
        public virtual ICollection<PostPollVoteSummary> PostPollVoteSummaries { get; set; }
        public virtual ICollection<PostPollVote> PostPollVotes { get; set; }
    }
}
