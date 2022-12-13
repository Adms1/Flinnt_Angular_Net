using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a list of audience groups.
    /// </summary>
    public partial class PostAudienceGroup : BaseEntity
    {
        public PostAudienceGroup()
        {
            Posts = new HashSet<Post>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int AudienceGroupId { get; set; }
        /// <summary>
        /// The user identifier who has created this group. Ref. User.UserId
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// The institute identifier this group belongs to. Ref. Institute.InstituteId
        /// </summary>
        public int? InstituteId { get; set; }
        /// <summary>
        /// The group name.
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// The group logo file path.
        /// </summary>
        public string GroupLogo { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// The filter data selected while creating this group. Store the data in the JSON format. {
        ///  &quot;version&quot;: &quot;1.0&quot;,
        ///  &quot;board&quot;: 1,
        ///  &quot;medium&quot;: 2,
        ///  &quot;standards&quot;: [1, 2, 3, 4],
        ///  &quot;divisions&quot;: [2,3,4],
        ///  &quot;roles&quot;: [&quot;student&quot;, &quot;teacher&quot;]
        /// }
        /// </summary>
        public string FilterData { get; set; }

        public virtual Institute Institute { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
