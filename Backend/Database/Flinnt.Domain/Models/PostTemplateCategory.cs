using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a list of post template categories.
    /// Migration:
    /// CategoryName &lt; post_templates.post_template_category
    /// </summary>
    public partial class PostTemplateCategory : BaseEntity
    {
        public PostTemplateCategory()
        {
            PostTemplates = new HashSet<PostTemplate>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public byte PostTemplateCategoryId { get; set; }
        /// <summary>
        /// The category name. Migartions: post_templates.post_template_category
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// If 1, the category is ready to use.
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// The category picture file path.
        /// </summary>
        public string CategoryPicture { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// The date and time when this entry was last updated.
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        public virtual ICollection<PostTemplate> PostTemplates { get; set; }
    }
}
