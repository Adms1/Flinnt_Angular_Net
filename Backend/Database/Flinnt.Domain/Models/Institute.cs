using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class Institute : BaseEntity
    {
        public Institute()
        {
            InstituteGroups = new HashSet<InstituteGroup>();
            InstituteSemesters = new HashSet<InstituteSemester>();
            UserInstitutes = new HashSet<UserInstitute>();
        }

        public int InstituteId { get; set; }
        public string InstituteName { get; set; }
        public byte InstituteTypeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
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

        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual InstituteType InstituteType { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<InstituteGroup> InstituteGroups { get; set; }
        public virtual ICollection<InstituteSemester> InstituteSemesters { get; set; }
        public virtual ICollection<UserInstitute> UserInstitutes { get; set; }
        public virtual ICollection<InstituteConfiguration> InstituteConfigurations { get; set; }
        public virtual ICollection<InstituteConfigureSession> InstituteConfigureSessions { get; set; }
    }
}
