using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a list of service which provides embedded media.
    /// </summary>
    public partial class MediaEmbedService : BaseEntity
    {
        public MediaEmbedService()
        {
            PostMedia = new HashSet<PostMedium>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public byte MediaEmbedServiceId { get; set; }
        /// <summary>
        /// The name of the service which allows embeding media.
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// If 1, the service is ready to use.
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// The display order of the service.
        /// </summary>
        public byte? DisplayOrder { get; set; }
        /// <summary>
        /// The properties of the service. Store data in JSON format.
        /// </summary>
        public string Properties { get; set; }
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
