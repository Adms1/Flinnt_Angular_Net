using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a list of post types.
    /// Migrations:
    /// PostTypeId &lt; post_type_master.post_type_id
    /// TypeName &lt; post_type_master.post_type
    /// IsActive &lt; post_type_master.is_active
    /// DisplayOrder &lt; post_type_master.srno
    /// </summary>
    public partial class PostType : BaseEntity
    {
        public PostType()
        {
            Posts = new HashSet<Post>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public byte PostTypeId { get; set; }
        /// <summary>
        /// The post type name. Migrations: post_type_master.post_type
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// If 1, the post type is ready to use. Migrations: post_type_master.is_active
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// The display order. Migrations: post_type_master.srno
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

        public virtual ICollection<Post> Posts { get; set; }
    }
}
