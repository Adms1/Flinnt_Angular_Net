using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    /// <summary>
    /// This entity stores information about parents.
    /// </summary>
    public partial class Parent: BaseEntity
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// The user identifier this parent belongs to.
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// The first name of the parent 1.
        /// </summary>
        public string Parent1FirstName { get; set; }
        /// <summary>
        /// The last name of the parent 1.
        /// </summary>
        public string Parent1LastName { get; set; }
        /// <summary>
        /// The relationship between the parent 1 and a student.
        /// </summary>
        public string Parent1Relationship { get; set; }
        /// <summary>
        /// The email address of parent 1.
        /// </summary>
        public string Parent1EmailId { get; set; }
        /// <summary>
        /// The mobile no. of parent 1.
        /// </summary>
        public string Parent1MobileNo { get; set; }
        /// <summary>
        /// If 1, only parent 1 relationship is there.
        /// </summary>
        public byte SingleParent { get; set; }
        /// <summary>
        /// The first name of the parent 2.
        /// </summary>
        public string Parent2FirstName { get; set; }
        /// <summary>
        /// The last name of the parent 2.
        /// </summary>
        public string Parent2LastName { get; set; }
        /// <summary>
        /// The relationship between the parent 2 and a student.
        /// </summary>
        public string Parent2Relationship { get; set; }
        /// <summary>
        /// The email address of parent 2.
        /// </summary>
        public string Parent2EmailId { get; set; }
        /// <summary>
        /// The mobile no. of parent 2.
        /// </summary>
        public string Parent2MobileNo { get; set; }
        /// <summary>
        /// The primary email address to contact.
        /// </summary>
        public string PrimaryEmailId { get; set; }
        /// <summary>
        /// The primary mobile not to contact.
        /// </summary>
        public string PrimaryMobileNo { get; set; }
        /// <summary>
        /// The address line 1.
        /// </summary>
        public string AddressLine1 { get; set; }
        /// <summary>
        /// The address line 2.
        /// </summary>
        public string AddressLine2 { get; set; }
        /// <summary>
        /// The city identifier this parent belongs to.
        /// </summary>
        public int? CityId { get; set; }
        /// <summary>
        /// The state identifier this parent belongs to.
        /// </summary>
        public int? StateId { get; set; }
        /// <summary>
        /// The country identifier this parent belongs to.
        /// </summary>
        public byte? CountryId { get; set; }
        /// <summary>
        /// The pincode of the address.
        /// </summary>
        public string Pincode { get; set; }
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
