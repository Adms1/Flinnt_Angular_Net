using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flinnt.Business.ViewModels
{
    public class StandardViewModel
    {
        public byte StandardId { get; set; }
        public string StandardName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsOptional { get; set; }
    }
}
