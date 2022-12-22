using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    public partial class PostUserViewModel
    {
        public long PostUserId { get; set; }
        public int PostId { get; set; }
        public long UserId { get; set; }
        public bool? IsView { get; set; }
        public DateTime? ViewDateTime { get; set; }
        public bool? Likes { get; set; }
        public DateTime? LikeDateTime { get; set; }
        public bool? Bookmark { get; set; }
        public DateTime? BookmarkDateTime { get; set; }
    }
}
