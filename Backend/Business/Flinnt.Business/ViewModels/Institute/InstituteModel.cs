using System;
using System.ComponentModel.DataAnnotations;

namespace Flinnt.Business.ViewModels
{
    public class InstituteModel
    {
        public int InstituteId { get; set; }
        public string InstituteName { get; set; }
        public byte InstituteTypeId { get; set; }
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255)]
        public string LastName { get; set; }
        [Required]
        [StringLength(255)]
        public string EmailId { get; set; }
        [Required]
        [StringLength(10)]
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public byte? CountryId { get; set; }
        public string Pincode { get; set; }
        public string Website { get; set; }
        public string PublicPageName { get; set; }
        public bool? PageNameChanged { get; set; }
        public string DisplayPicture { get; set; }
        public string BannerPicture { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public  UserModel User { get; set; }
    }
}