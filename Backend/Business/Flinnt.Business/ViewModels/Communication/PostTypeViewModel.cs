using System;
using System.Collections.Generic;

namespace Flinnt.Business.ViewModels
{
    public partial class PostTypeViewModel
    {
        public PostTypeViewModel()
        {
        }

        public byte PostTypeId { get; set; }
        public string TypeName { get; set; }
        public bool? IsActive { get; set; }
        public byte? DisplayOrder { get; set; }
    }
}
