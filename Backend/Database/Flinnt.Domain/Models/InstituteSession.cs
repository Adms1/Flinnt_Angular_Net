using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a session list for institutes.
    /// </summary>
    public partial class InstituteSession: BaseEntity
    {
        public InstituteSession()
        {
            UserInstituteGroups = new HashSet<UserInstituteGroup>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int InstituteSessionId { get; set; }
        /// <summary>
        /// The institute batch identifier this session belongs to.
        /// </summary>
        public int InstituteBatchId { get; set; }
        /// <summary>
        /// The session name.
        /// </summary>
        public string SessionName { get; set; }
        /// <summary>
        /// The time when the session starts.
        /// </summary>
        public TimeSpan? StartTime { get; set; }
        /// <summary>
        /// The time when the session ends.
        /// </summary>
        public TimeSpan? EndTime { get; set; }
        /// <summary>
        /// If 1, the session is ready to use.
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// The date and time when this entry was last updated.
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        public virtual InstituteBatch InstituteBatch { get; set; }
        public virtual ICollection<UserInstituteGroup> UserInstituteGroups { get; set; }
    }
}
