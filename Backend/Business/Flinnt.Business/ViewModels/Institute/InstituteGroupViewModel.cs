using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flinnt.Business.ViewModels
{
    public class InstituteGroupViewModel
    {
        public int InstituteGroupId { get; set; }
        public int InstituteId { get; set; }
        public byte? BoardId { get; set; }
        public byte? MediumId { get; set; }
        public byte? StandardId { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsActive { get; set; }
        public StandardViewModel StandardViewModel { get; set; }
        public List<StandardViewModel> Standards { get; set; }
    }
}
