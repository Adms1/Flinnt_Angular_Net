using System;
using System.ComponentModel.DataAnnotations;

namespace Flinnt.Business.ViewModels
{
    public class InstituteTypeViewModel
    {
        public byte InstTypeId { get; set; }
        public string TypeName { get; set; }
        public bool? IsActive { get; set; }
    }
}