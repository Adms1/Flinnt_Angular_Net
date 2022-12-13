using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    public partial class MediaType : BaseEntity
    {
        public MediaType()
        {
            PostMedia = new HashSet<PostMedium>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public byte MediaTypeId { get; set; }
        /// <summary>
        /// The media type name. Migrations: post_type_master.post_type
        /// </summary>
        public string MediaType1 { get; set; }
        /// <summary>
        /// If 1, the media type is ready to use.
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// The display order of the media type.
        /// </summary>
        public byte? DisplayOrder { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// The date and time when this entry was last updated.
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        public virtual ICollection<PostMedium> PostMedia { get; set; }
    }
}
