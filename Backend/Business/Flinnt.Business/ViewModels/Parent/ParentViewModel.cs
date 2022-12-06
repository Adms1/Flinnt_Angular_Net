using Flinnt.Domain;
using System;
using System.Collections.Generic;

namespace Flinnt.Business.ViewModels
{
    public partial class ParentViewModel
    {
        public ParentViewModel()
        {
            ImportSummary = new List<ParentImportSummary>();
        }
        public int ParentId { get; set; }
        public long UserId { get; set; }
        public string Parent1FirstName { get; set; }
        public string Parent1LastName { get; set; }
        public string Parent1Relationship { get; set; }
        public string Parent1EmailId { get; set; }
        public string Parent1MobileNo { get; set; }
        public byte SingleParent { get; set; }
        public string Parent2FirstName { get; set; }
        public string Parent2LastName { get; set; }
        public string Parent2Relationship { get; set; }
        public string Parent2EmailId { get; set; }
        public string Parent2MobileNo { get; set; }
        public string PrimaryEmailId { get; set; }
        public string PrimaryMobileNo { get; set; }

        //contact info
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public byte? CountryId { get; set; }
        public string Pincode { get; set; }
        public DateTime? CreateDateTime { get; set; }

        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public List<ParentImportSummary> ImportSummary { get; set; }
        public string ImportStatus { get; set; }
        public int InstituteId { get; set; }
        public int LoggedUserId { get; set; }
    }

    public class ParentImportSummary 
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
}
