using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flinnt.Business.ViewModels
{
    public class UserParentChildRelationshipModel
    {
        public int UserParentChildRelationshipId { get; set; }
        public long ParentUserId { get; set; }
        public byte ParentUserTypeId { get; set; }
        public long ChildUserId { get; set; }
        public byte ChildUserTypeId { get; set; }
        public int InstituteId { get; set; }
    }
}