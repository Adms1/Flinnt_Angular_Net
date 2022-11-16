using Flinnt.Business.Enums.General;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Flinnt.Business.ViewModels
{
    public class UserInstituteModel
    {
        public int UserInstituteId { get; set; }
        public long UserId { get; set; }
        public int InstituteId { get; set; }
        public byte UserTypeId { get; set; }
        public byte? RoleId { get; set; }
        public bool? IsActive { get; set; }
    }
}