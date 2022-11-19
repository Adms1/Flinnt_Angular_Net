using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flinnt.Business.ViewModels
{
    public class UserInstituteGroupModel
    {
        public int UserInstituteGroupId { get; set; }
        public long UserId { get; set; }
        public int InstituteId { get; set; }
        public int? InstituteGroupId { get; set; }
        public int? InstituteDivisionId { get; set; }
        public int? InstituteSemesterId { get; set; }
        public short? AcademicYearId { get; set; }
        public int? InstituteBatchId { get; set; }
        public int? InstituteSessionId { get; set; }
    }
}