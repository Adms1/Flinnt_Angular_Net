using System;
using System.Collections.Generic;

namespace Flinnt.Business.ViewModels
{
    public partial class ParentViewModel
    {
        public int ParentId { get; set; }
        public long UserId { get; set; }
        public string Parent1FirstName { get; set; }
        public string Parent1LastName { get; set; }
        public string Parent1Relationship { get; set; }
        public string Parent1EmailId { get; set; }
        public string Parent1MobileNo { get; set; }
        public byte? SingleParent { get; set; }
        public string Parent2FirstName { get; set; }
        public string Parent2LastName { get; set; }
        public string Parent2Relationship { get; set; }
        public string Parent2EmailId { get; set; }
        public string Parent2MobileNo { get; set; }
        public string PrimaryEmailId { get; set; }
        public string PrimaryMobileNo { get; set; }
    }
}
