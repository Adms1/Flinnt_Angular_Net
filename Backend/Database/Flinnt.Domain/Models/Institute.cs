using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores the institute information.
    /// Migration:
    /// InstituteName &lt; users.user_school_name
    /// FirstName &lt; users.user_firstname
    /// LastName &lt; users.user_lastname
    /// EmailId &lt; users.user_email
    /// MobileNo &lt; users.user_mobile
    /// Address &lt; users.user_address
    /// CityId &lt; users.user_city
    /// StateId &lt; users.user_state
    /// CountryId &lt; users.user_country
    /// Pincode &lt; users.user_pincode
    /// Website &lt; users.user_website
    /// PublicPageName &lt; users.user_name
    /// PageNameChanged &lt; users.user_name_changed
    /// DisplayPicture &lt; users.user_picture
    /// BannerPicture &lt; users.user_school_banner
    /// </summary>
    public partial class Institute: BaseEntity
    {
        public Institute()
        {
            AcademicYears = new HashSet<AcademicYear>();
            InstituteConfigurations = new HashSet<InstituteConfiguration>();
            InstituteConfigureSessions = new HashSet<InstituteConfigureSession>();
            InstituteGroups = new HashSet<InstituteGroup>();
            InstituteSemesters = new HashSet<InstituteSemester>();
            UserInstituteGroups = new HashSet<UserInstituteGroup>();
            UserInstitutes = new HashSet<UserInstitute>();
            UserParentChildRelationships = new HashSet<UserParentChildRelationship>();
        }

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int InstituteId { get; set; }
        /// <summary>
        /// The institute name.
        /// </summary>
        public string InstituteName { get; set; }
        /// <summary>
        /// The institute type identifier this institute belongs to. Ref.: InstituteType.InstituteTypeId
        /// </summary>
        public byte? InstituteTypeId { get; set; }
        /// <summary>
        /// The group structure identifier this institute belongs to.
        /// </summary>
        public byte? GroupStructureId { get; set; }
        /// <summary>
        /// The contact person first name.
        /// Migrate: users.user_firstname
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The contact person last name.
        /// Migrate: users.user_lastname
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The contact person email address.
        /// Migrate: users.user_email
        /// </summary>
        public string EmailId { get; set; }
        /// <summary>
        /// The contact person mobile number.
        /// Migrate: users.user_mobile
        /// </summary>
        public string MobileNo { get; set; }
        /// <summary>
        /// The institute address.
        /// Migrate: users.user_address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// The city identifier this institute address belongs to. Ref.: City.CityId
        /// Migrate: users.user_city
        /// </summary>
        public int? CityId { get; set; }
        /// <summary>
        /// The state identifier this institute address belongs to. Ref.: State.StateId
        /// Migrate: users.user_state
        /// </summary>
        public int? StateId { get; set; }
        /// <summary>
        /// The country identifier this institute address belongs to.  Ref.: Country.CountryId
        /// Migrate: users.user_country
        /// </summary>
        public byte? CountryId { get; set; }
        /// <summary>
        /// The pincode of the institute address.
        /// Migrate: users.user_pincode
        /// </summary>
        public string Pincode { get; set; }
        /// <summary>
        /// The website address of the institute.
        /// Migrate: users.user_website
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// The public page name (part of the permanent URL) of the institute.
        /// Migrate: users.user_name
        /// </summary>
        public string PublicPageName { get; set; }
        /// <summary>
        /// If 1, the institute has already changed his public page name once.
        /// Migrate: users.user_name_changed
        /// </summary>
        public bool? PageNameChanged { get; set; }
        /// <summary>
        /// The profile picture path of the institute.
        /// Migrate: users.user_picture
        /// </summary>
        public string DisplayPicture { get; set; }
        /// <summary>
        /// The institute banner image path of the institute.
        /// Migrate: users.user_school_banner
        /// </summary>
        public string BannerPicture { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// The date and time when this entry was last updated.
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual GroupStructure GroupStructure { get; set; }
        public virtual InstituteType InstituteType { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<AcademicYear> AcademicYears { get; set; }
        public virtual ICollection<InstituteConfiguration> InstituteConfigurations { get; set; }
        public virtual ICollection<InstituteConfigureSession> InstituteConfigureSessions { get; set; }
        public virtual ICollection<InstituteGroup> InstituteGroups { get; set; }
        public virtual ICollection<InstituteSemester> InstituteSemesters { get; set; }
        public virtual ICollection<UserInstituteGroup> UserInstituteGroups { get; set; }
        public virtual ICollection<UserInstitute> UserInstitutes { get; set; }
        public virtual ICollection<UserParentChildRelationship> UserParentChildRelationships { get; set; }
    }
}
