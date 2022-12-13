using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores a list of post templates.
    /// Migration:
    /// TemplateName &lt; post_templates.template_name
    /// TemplateTitle &lt; post_templates.template_title
    /// Description &lt; post_templates.post_template_description
    /// IsActive &lt; post_templates.post_template_active
    /// DisplayOrder &lt; post_templates.post_template_srno
    /// CreateDateTime &lt; post_templates.post_template_inserted
    /// UpdateDateTime &lt; post_templates.post_template_updated
    /// </summary>
    public partial class PostTemplate :BaseEntity
    {
        public PostTemplate()
        {
            Posts = new HashSet<Post>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public short PostTemplateId { get; set; }
        /// <summary>
        /// The template name. Migrations: post_templates.template_name
        /// </summary>
        public string TemplateName { get; set; }
        public string TemplateTitle { get; set; }
        /// <summary>
        /// The template body. Migrations: post_templates.post_template_description
        /// </summary>
        public string TemplateBody { get; set; }
        /// <summary>
        /// The category identifier this template belongs to. Ref. PostTemplateCategory.PostTemplateCategoryId
        /// </summary>
        public byte? PostTemplateCategoryId { get; set; }
        /// <summary>
        /// If 1, the template is ready to use. Migrations: post_templates.post_template_active
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// The display order of the template. Migartions: post_templates.post_template_srno
        /// </summary>
        public short? DisplayOrder { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// The date and time when this entry was last updated.
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        public virtual PostTemplateCategory PostTemplateCategory { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
