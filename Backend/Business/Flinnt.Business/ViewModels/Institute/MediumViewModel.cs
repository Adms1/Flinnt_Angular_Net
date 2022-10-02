using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flinnt.Business.ViewModels
{
    public class MediumViewModel
    {
        public byte MediumId { get; set; }
        public string MediumName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsOptional { get; set; }
    }
}
