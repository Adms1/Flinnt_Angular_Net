using System;
using System.Collections.Generic;

namespace Flinnt.Business.ViewModels
{
    public partial class PostTemplateViewModel
    {
        public PostTemplateViewModel()
        {
        }

        public short PostTemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateTitle { get; set; }
        public string TemplateBody { get; set; }
        public byte? PostTemplateCategoryId { get; set; }
        public bool? IsActive { get; set; }
        public short? DisplayOrder { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public PostTemplateCategoryViewModel PostTemplateCategory { get; set; }
    }
}
