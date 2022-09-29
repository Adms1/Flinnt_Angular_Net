using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flinnt.Business.ViewModels.Institute
{
    public class BoardViewModel
    {
        public byte BoardId { get; set; }
        public string BoardName { get; set; }
        public string BoardShortName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsOptional { get; set; }
    }
}
