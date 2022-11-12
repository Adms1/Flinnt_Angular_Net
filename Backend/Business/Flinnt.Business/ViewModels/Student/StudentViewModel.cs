using System;
using System.Collections.Generic;
using System.Reflection;

namespace Flinnt.Business.ViewModels
{
    public partial class StudentViewModel
    {
        public int StudentId { get; set; }
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public byte? GenderId { get; set; }
        public string RollNo { get; set; }
        public string Grno { get; set; }

        // other info
        public int BoardId { get; set; }
        public int MediumId { get; set; }
        public int StandardId { get; set; }
        public int DivisionId { get; set; }
    }
}
