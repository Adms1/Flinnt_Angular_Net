using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a list of communications done inside an institution.
    /// Migration:
    /// Title &lt; posts.title
    /// Description &lt; posts.description
    /// PostTypeId &lt; posts.post_type
    /// PublishDateTime &lt; posts.publish_date
    /// UserId &lt; posts.user_id
    /// InstituteId &lt; posts.module.course_owner (connected through User -&gt; Institute)
    /// PostTemplateId &lt; posts.template_id
    /// IsApprove &lt; posts.approved
    /// ApproveByUserId &lt; posts.approvedby
    /// 
    /// </summary>
    public partial class Post:BaseEntity
    {
        public Post()
        {
            PostComments = new HashSet<PostComment>();
            PostMedia = new HashSet<PostMedium>();
            PostPolls = new HashSet<PostPoll>();
            PostUsers = new HashSet<PostUser>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int PostId { get; set; }
        /// <summary>
        /// The post title. Migrations: posts.title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The post body. Migrations: posts.description
        /// </summary>
        public string MessageBody { get; set; }
        /// <summary>
        /// The type identifier this post belongs to. Ref. PostType.PostTypeId. Migrations: posts.post_type
        /// </summary>
        public byte PostTypeId { get; set; }
        /// <summary>
        /// The date and time when this post should be visible to audience. Migrations: posts.publish_date
        /// </summary>
        public DateTime PublishDateTime { get; set; }
        /// <summary>
        /// The user identifier who has make this post. Ref. User.UserId. Migrations: posts.user_id
        /// </summary>
        public long? UserId { get; set; }
        /// <summary>
        /// The institute identifier this post belongs to. Ref. Institute.InstituteId. Migartions: posts.module(course.course_owner)
        /// </summary>
        public int? InstituteId { get; set; }
        /// <summary>
        /// The template identifier this post belongs to. Ref. PostTemplate.PostTemplateId. Migrations: posts.template_id
        /// </summary>
        public short? PostTemplateId { get; set; }
        /// <summary>
        /// If 1, the approval is required before making this post public.
        /// </summary>
        public bool? ApprovalRequire { get; set; }
        /// <summary>
        /// If 1, the post is approved and can be displayed to the audience. Migrations: posts.approved
        /// </summary>
        public bool? IsApprove { get; set; }
        /// <summary>
        /// The identifier of the user who has approved this post. Ref. User.UserId. Migrations: posts.approvedby
        /// </summary>
        public long? ApproveByUserId { get; set; }
        /// <summary>
        /// The date and time when the post has been approved.
        /// </summary>
        public DateTime? ApproveDateTime { get; set; }
        /// <summary>
        /// If 1, the post will be visible to all the users of the institution.
        /// </summary>
        public bool? Broadcast { get; set; }
        public int? AudienceGroupId { get; set; }
        /// <summary>
        /// The date and time when the post has been deleted. Do not display posts, if the value is not null.
        /// </summary>
        public DateTime? DeleteDateTime { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// The date and time when this entry was last updated.
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }
        /// <summary>
        /// The Client IP address from where the request was initiated.
        /// </summary>
        public string ClientIp { get; set; }
        /// <summary>
        /// The Client Device from where the request was initiated.
        /// </summary>
        public string ClientDevice { get; set; }

        public virtual User ApproveByUser { get; set; }
        public virtual PostAudienceGroup AudienceGroup { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual PostTemplate PostTemplate { get; set; }
        public virtual PostType PostType { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }
        public virtual ICollection<PostMedium> PostMedia { get; set; }
        public virtual ICollection<PostPoll> PostPolls { get; set; }
        public virtual ICollection<PostUser> PostUsers { get; set; }
    }
}
