using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flinnt.Business.ViewModels.Institute
{
    public class GroupStructureViewModel
    {
        public byte GroupStructureId { get; set; }
        public string StructureTitle { get; set; }
        public string Description { get; set; }
        public byte? DisplayOrder { get; set; }
        public bool? IsActive { get; set; }
    }
}