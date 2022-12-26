using System;
using System.Collections.Generic;

namespace Flinnt.Business.ViewModels
{
    public partial class MediaEmbedServiceViewModel
    {
        public MediaEmbedServiceViewModel()
        {
        }

        public byte MediaEmbedServiceId { get; set; }
        public string ServiceName { get; set; }
        public bool? IsActive { get; set; }
        public byte? DisplayOrder { get; set; }
        public string Properties { get; set; }
    }
}
