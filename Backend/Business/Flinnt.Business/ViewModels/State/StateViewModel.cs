using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Business.ViewModels
{
    public class StateViewModel
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public byte CountryId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}
