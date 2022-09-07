using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Business.ViewModels
{
    public class CountryViewModel
    {
        public byte CountryId { get; set; }
        public string CountryName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}
