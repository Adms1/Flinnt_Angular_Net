using System;
using System.Collections.Generic;

namespace Flinnt.Business.ViewModels
{
    public partial class MediaTypeViewModel
    {
        public MediaTypeViewModel()
        {
        }

        public byte MediaTypeId { get; set; }
        public string MediaType1 { get; set; }
        public bool? IsActive { get; set; }
        public byte? DisplayOrder { get; set; }
    }
}
