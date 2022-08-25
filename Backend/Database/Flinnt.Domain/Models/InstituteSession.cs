using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class InstituteSession
    {
        public int InstituteSessionId { get; set; }
        public int InstituteBatchId { get; set; }
        public string SessionName { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual InstituteBatch InstituteBatch { get; set; }
    }
}
