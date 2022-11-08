using System;
using System.Collections.Generic;

namespace Flinnt.Domain
{
    public partial class Student: BaseEntity
    {
        /// <summary>
        /// The unique identifier.
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// The user identifier this student belongs to.
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// The first name of the student.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The last name of the student.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The email address of the student
        /// </summary>
        public string EmailId { get; set; }
        /// <summary>
        /// The mobile number of the student
        /// </summary>
        public string MobileNo { get; set; }
        /// <summary>
        /// The gender identifier of the student
        /// </summary>
        public byte? GenderId { get; set; }
        /// <summary>
        /// The enrollment number of the student.
        /// </summary>
        public string RollNo { get; set; }
        /// <summary>
        /// The general register number of the student.
        /// </summary>
        public string Grno { get; set; }
        /// <summary>
        /// The date and time when this entry was done.
        /// </summary>
        public DateTime? CreateDateTime { get; set; }
        /// <summary>
        /// The date and time when this entry was last updated.
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        public virtual Gender Gender { get; set; }
        public virtual User User { get; set; }
    }
}
