using System;
using System.Collections.Generic;

namespace Flinnt.Business.ViewModels
{
    public partial class PostLogViewModel
    {
        public PostLogViewModel()
        {
        }

        public long PostLogId { get; set; }
        public int PostId { get; set; }
        public long? UserId { get; set; }
        public string ActionType { get; set; }
        public DateTime? ActionDateTime { get; set; }
        public string ExtraInformation { get; set; }
        public string ClientIp { get; set; }
        public string ClientDevice { get; set; }
    }
}
