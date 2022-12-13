using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entitity stores a list of comments posted by users on a post.
    /// Migration:
    /// PostId &lt; post_comments.post_id
    /// CommentText &lt; post_comments.comment_text
    /// UserID &lt; post_comments.comment_user_id
    /// Approve &lt; post_comments.comment_approve
    /// ApproveUserId &lt; post_comments.comment_approve_by
    /// ApproveDateTime &lt; post_comments.comment_approve_date
    /// CreateDateTime &lt; post_comments.comment_date
    /// </summary>
    public partial class PostComment : BaseEntity
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int PostCommentId { get; set; }
        /// <summary>
        /// The post identifier this comment belongs to. Ref. Post.PostId. Migrations: post_comments.post_id
        /// </summary>
        public int PostId { get; set; }
        /// <summary>
        /// The comment text. Migrations: post_comments.comment_text
        /// </summary>
        public string CommentText { get; set; }
        /// <summary>
        /// The user identifier who has posted this comment. Ref. User.UserId. Migrations: post_comments.comment_user_id
        /// </summary>
        public long? UserId { get; set; }
        /// <summary>
        /// If 1, the comment can be displayed. Migrations: post_comments.comment_approve
        /// </summary>
        public bool? Approve { get; set; }
        /// <summary>
        /// The user identifier who has approved this comment. This will be null if no approval required. Ref. User.UserId. Migrations: post_comments.comment_approve_by
        /// </summary>
        public long? ApproveUserId { get; set; }
        /// <summary>
        /// The date and time when the comment was approved. This will be null if not approval required. Migrations: post_comments.comment_approve_date
        /// </summary>
        public DateTime? ApproveDateTime { get; set; }
        /// <summary>
        /// The date and time when this entry was done. Migrations: post_comments.comment_date
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// The date and time when this entry was last updated.
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        public virtual User ApproveUser { get; set; }
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
