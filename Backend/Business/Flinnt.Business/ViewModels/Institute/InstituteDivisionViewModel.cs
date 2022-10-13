using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flinnt.Business.ViewModels
{
    public class InstituteDivisionViewModel
    {
        public int InstituteDivisionId { get; set; }
        public string DivisionName { get; set; }
        public int InstituteGroupId { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsActive { get; set; }
        public StandardViewModel StandardViewModel { get; set; }
        public List<StandardViewModel> Standards { get; set; }
    }
}
