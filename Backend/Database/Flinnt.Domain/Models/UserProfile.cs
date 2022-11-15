using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores the user profile.
    /// Migration:
    /// UserProfiles
    ///  Stores user profile information.
    /// 
    /// FirstName	 &lt; users.user_firstname
    /// LastName	&lt; users.user_lastname
    /// EmailId &lt; users.user_email
    /// MobileNo &lt; users.user_mobile
    /// DOB &lt; users.user_birthdate
    /// GenderId &lt; users.user_gender
    /// Address &lt; users.user_address
    /// CityId &lt; users.user_city
    /// StateId &lt; users.user_state
    /// CountryId &lt; users.user_country
    /// Pincode &lt; users.user_pincode
    /// DisplayPicture &lt; users.user_picture
    /// PublicId &lt; users.user_public_id
    /// PublicIdSalt &lt; users.user_public_id_salt
    /// </summary>
    public partial class UserProfile : BaseEntity
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int UserProfileId { get; set; }
        /// <summary>
        /// The user identifier this profile belongs to. Ref.: Users.UserId
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// The user first name.
        /// Migration: users.user_firstname
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The user last name.
        /// Migration: users.user_lastname
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The user email address.
        /// Migration: users.user_email
        /// </summary>
        public string EmailId { get; set; }
        /// <summary>
        /// The user mobile number.
        /// Migration: users.user_mobile
        /// </summary>
        public string MobileNo { get; set; }
        /// <summary>
        /// The user date of birth.
        /// Migration: users.user_birthdate
        /// </summary>
        public DateTime? Dob { get; set; }
        /// <summary>
        /// The gender identifier this profile belongs to. Ref.: Genders.GenderId
        /// Migrate: users.user_gender
        /// </summary>
        public byte? GenderId { get; set; }
        /// <summary>
        /// The residential address.
        /// Migrate: users.user_address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// The city identifier this profile belongs to. Ref.: City.CityId
        /// Migrate: users.user_city
        /// </summary>
        public int? CityId { get; set; }
        /// <summary>
        /// The state identifier this profile belongs to. Ref.: State.StateId
        /// Migrate: users.user_state
        /// </summary>
        public int? StateId { get; set; }
        /// <summary>
        /// The country identifier this profile belongs to. Ref.: Country.CountryId
        /// Migrate: users.user_country
        /// </summary>
        public byte? CountryId { get; set; }
        /// <summary>
        /// The residential address pincode.
        /// Migrate: users.user_pincode
        /// </summary>
        public string Pincode { get; set; }
        /// <summary>
        /// The display picture of the user.
        /// Migrate: users.user_picture
        /// </summary>
        public string DisplayPicture { get; set; }
        /// <summary>
        /// The unique public identifier. These can be shared publically.
        /// Migrate: users.user_public_id
        /// </summary>
        public string PublicId { get; set; }
        /// <summary>
        /// The secret key used to generate the public identifier.
        /// users.user_public_id_salt
        /// </summary>
        public string PublicIdSalt { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// The date and time when this entry was last updated.
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }
    }
}
