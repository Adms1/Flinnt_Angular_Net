using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores post and user mapping details.
    /// Migration:
    /// PostId &lt; post_user_detail.post_id
    /// UserId &lt; post_user_detail.user_id
    /// IsView &lt; post_user_detail.is_view
    /// ViewDateTime &lt; post_user_detail.view_date
    /// Likes &lt; post_user_detail.is_agree
    /// LikeDateTime &lt; post_user_detail.aggree_date
    /// Bookmark &lt; post_user_detail.is_bookmark
    /// BookmarkDateTime &lt; post_user_detail.bookmark_date
    /// </summary>
    public partial class PostUser : BaseEntity
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public long PostUserId { get; set; }
        /// <summary>
        /// The post identifier this user belongs to. Ref. Post.PostId. Migrations: post_user_detail.post_id
        /// </summary>
        public int PostId { get; set; }
        /// <summary>
        /// The user identifier this post belongs to. Ref. User.UserId. Migrations: post_user_detail.user_id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// If 1, the post has been viewed by the user. Migrations: post_user_detail.is_view
        /// </summary>
        public bool? IsView { get; set; }
        /// <summary>
        /// The date and time when the user has read this post. Migrations: post_user_detail.view_date
        /// </summary>
        public DateTime? ViewDateTime { get; set; }
        /// <summary>
        /// If 1, the user has liked this post. Migrations: post_user_detail.is_agree
        /// </summary>
        public bool? Likes { get; set; }
        /// <summary>
        /// The date and time when user liked this post. Migrations: post_user_detail.agree_date
        /// </summary>
        public DateTime? LikeDateTime { get; set; }
        /// <summary>
        /// If 1, the post has been added to the bookmark list by the user. Migrations: post_user_detail.is_bookmark
        /// </summary>
        public bool? Bookmark { get; set; }
        /// <summary>
        /// The date and time when user added this post to the bookmark list. Migrations: post_user_detail.bookmark_date
        /// </summary>
        public DateTime? BookmarkDateTime { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
