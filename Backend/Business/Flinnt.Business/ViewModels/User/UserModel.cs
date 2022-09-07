using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flinnt.Business.ViewModels
{
    public class ApplicationUser : IdentityUser
    {
        public long UserId { get; set; }
     
        [NotMapped]
        public string Password { get; set; }
    }
}