using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    public partial class PostPollVoteViewModel
    {
        public int PostPollVote1 { get; set; }
        public int PostPollId { get; set; }
        public int PostPollOptionId { get; set; }
        public long UserId { get; set; }
        public DateTime? VoteDateTime { get; set; }
        public string ClientIp { get; set; }
        public string ClientDevice { get; set; }
    }
}
