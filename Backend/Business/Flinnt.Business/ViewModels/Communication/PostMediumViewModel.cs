using System;
using System.Collections.Generic;

namespace Flinnt.Business.ViewModels
{
    public partial class PostMediumViewModel
    {
        public PostMediumViewModel()
        {
        }

        public int PostMediaId { get; set; }
        public int PostId { get; set; }
        public byte MediaTypeId { get; set; }
        public string FilePath { get; set; }
        public byte? MediaEmbedServiceId { get; set; }
        public int? SizeBytes { get; set; }
        public string MimeType { get; set; }
        public string Properties { get; set; }
        public int? DisplayOrder { get; set; }
        public DateTime? CreateDateTime { get; set; }
    }
}
