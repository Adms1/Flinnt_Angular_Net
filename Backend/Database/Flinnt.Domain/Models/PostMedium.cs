using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a list of media attached with a post.
    /// Migration:
    /// PostId &lt; post_detail.post_id
    /// MediaTypeId &lt; post_detail.post_type_id, poll.add_content_type
    /// MediaFile &lt; post_detail.intro, poll.add_content
    /// MediaProperties &lt; to_json(post_detail.image_width, post_detail.image_height, post_detail.video_thumbnail, post_detail.aud_vid_size, post_detail.aud_vid_duration, poll.image_width, poll.image_height)
    /// DisplayOrder &lt; post_detail.post_order
    /// 
    /// 
    /// </summary>
    public partial class PostMedium : BaseEntity
    {
        public int PostMediaId { get; set; }
        /// <summary>
        /// The post identifier this media belongs to. Ref. Post.PostId. Migrations: post_detail.post_id
        /// </summary>
        public int PostId { get; set; }
        /// <summary>
        /// The identifier of the type of media. Ref. MediaType.MediaTypeId. Migartions: post_detail.post_type_id, poll.add_content_type
        /// </summary>
        public byte MediaTypeId { get; set; }
        /// <summary>
        /// The file path of the attached media. Migrations: post_detail.intro, poll.add_content
        /// 
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// The service identifier which provided embedded media. Ref. MediaEmbedService.MediaEmbedServiceId
        /// </summary>
        public byte? MediaEmbedServiceId { get; set; }
        /// <summary>
        /// The size of media file in bytes.
        /// </summary>
        public int? SizeBytes { get; set; }
        /// <summary>
        /// The mime type of the media file.
        /// </summary>
        public string MimeType { get; set; }
        /// <summary>
        /// The media properties in JSON format.
        /// for image = {&quot;width&quot;: 100, &quot;height&quot;: 80}, video = {&quot;duration&quot;: 10}, audio = {&quot;duration&quot;: 10}
        /// 
        /// Migrations: to_json(post_detail.image_width, post_detail.image_height, post_detail.video_thumbnail, post_detail.aud_vid_size, post_detail.aud_vid_duration, poll.image_width, poll.image_height)
        /// </summary>
        public string Properties { get; set; }
        /// <summary>
        /// The display order of the media. Migrations: post_detail.post_order
        /// </summary>
        public int? DisplayOrder { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }

        public virtual MediaEmbedService MediaEmbedService { get; set; }
        public virtual MediaType MediaType { get; set; }
        public virtual Post Post { get; set; }
    }
}
