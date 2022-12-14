using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    public partial class PostTemplateCategoryViewModel
    {
        public PostTemplateCategoryViewModel()
        {
            PostTemplates = new List<PostTemplateViewModel>();
        }

        public byte PostTemplateCategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool? IsActive { get; set; }
        public string CategoryPicture { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public List<PostTemplateViewModel> PostTemplates { get; set; }
    }
}
