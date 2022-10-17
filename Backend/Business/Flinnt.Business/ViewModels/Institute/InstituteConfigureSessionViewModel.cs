using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flinnt.Business.ViewModels
{
    public class InstituteConfigureSessionViewModel
    {
        public int InstituteConfigureSessionId { get; set; }
        public byte? CurrentStep { get; set; }
        public byte? InstituteTypeId { get; set; }
        public byte? GroupStructureId { get; set; }
        public int? InstituteId { get; set; }
        public byte? BoardId { get; set; }
        public byte? MediumId { get; set; }
    }
}
