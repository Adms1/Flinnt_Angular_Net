using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    public partial class PostPollViewModel
    {
        public PostPollViewModel()
        {
        }

        public int PostPollId { get; set; }
        public int PostId { get; set; }
        public DateTime? EndDateTime { get; set; }
        public int? TotalVotesReceived { get; set; }
        public DateTime? CreateDateTime { get; set; }
    }
}
