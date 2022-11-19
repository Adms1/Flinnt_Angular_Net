using System;
using System.Collections.Generic;
using System.Reflection;

namespace Flinnt.Business.ViewModels
{
    public partial class StudentViewModel
    {
        public int StudentId { get; set; }
        public long? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public byte? GenderId { get; set; }
        public string RollNo { get; set; }
        public string Grno { get; set; }
        public string Dob { get; set; }
        public long? parentUserId { get; set; }

        // other info
        public int instituteId { get; set; }
        public int instituteGroupId { get; set; }
        public int instituteDivisionId { get; set; }
    }
}
