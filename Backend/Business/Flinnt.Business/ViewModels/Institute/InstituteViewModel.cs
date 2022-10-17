using System;
using System.ComponentModel.DataAnnotations;

namespace Flinnt.Business.ViewModels
{
    public class InstituteViewModel
    {
        public int InstituteId { get; set; }
        public string InstituteName { get; set; }
        public byte? InstituteTypeId { get; set; }
        public byte? GroupStructureId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public byte? CountryId { get; set; }
        public string Pincode { get; set; }
        public string Website { get; set; }
        public string PublicPageName { get; set; }
        public bool? PageNameChanged { get; set; }
        public string DisplayPicture { get; set; }
        public string BannerPicture { get; set; }
        public string Password { get; set; }
        public string OneTimePassword { get; set; }
    }
}