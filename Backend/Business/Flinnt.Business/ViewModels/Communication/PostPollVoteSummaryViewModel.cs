using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    public partial class PostPollVoteSummaryViewModel
    {
        public int PostPollVoteSummaryId { get; set; }
        public int PostPollId { get; set; }
        public int PostPollOptionId { get; set; }
        public int? VotesReceive { get; set; }
        public decimal? VotePercentage { get; set; }
    }
}
