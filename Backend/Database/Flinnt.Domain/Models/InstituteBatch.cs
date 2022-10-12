using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class InstituteBatch
    {
        public InstituteBatch()
        {
        }

        public int InstituteBatchId { get; set; }
        public string BatchName { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}
