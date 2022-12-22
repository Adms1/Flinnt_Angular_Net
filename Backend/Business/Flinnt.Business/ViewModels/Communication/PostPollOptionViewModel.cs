using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    public partial class PostPollOptionViewModel
    {
        public PostPollOptionViewModel()
        {
        }

        public int PostPollOptionId { get; set; }
        public int PostPollId { get; set; }
        public string OptionText { get; set; }
        public int? DisplayOrder { get; set; }
        public DateTime? CreateDateTime { get; set; }
    }
}
