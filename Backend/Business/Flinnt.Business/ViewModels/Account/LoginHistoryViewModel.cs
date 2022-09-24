using System;
using System.Collections.Generic;

namespace Flinnt.Business.ViewModels.Account
{
    public partial class LoginHistoryViewModel
    {
        public int LoginHistoryId { get; set; }
        public long? UserId { get; set; }
        public DateTime? LoginDateTime { get; set; }
        public bool? IsLogout { get; set; }
        public string ClientIp { get; set; }
        public string ClientDevice { get; set; }
        public string AccessUrl { get; set; }
    }
}
