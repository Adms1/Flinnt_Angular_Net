using System;
using System.Collections.Generic;

#nullable disable

namespace Flinnt.Domain
{
    public partial class UserProfile : BaseEntity
    {
        public int UserProfileId { get; set; }
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public DateTime? Dob { get; set; }
        public byte? GenderId { get; set; }
        public string Address { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public byte? CountryId { get; set; }
        public string Pincode { get; set; }
        public string DisplayPicture { get; set; }
        public string PublicId { get; set; }
        public string PublicIdSalt { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual State State { get; set; }
        public virtual User User { get; set; }
    }
}
