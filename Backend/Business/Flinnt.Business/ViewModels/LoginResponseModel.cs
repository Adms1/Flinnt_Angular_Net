using Flinnt.Business.ViewModels.Account;
using System.ComponentModel.DataAnnotations;

namespace Flinnt.Business.ViewModels
{
    public class LoginResponseModel
    {        
        public ApplicationUser ApplicationUser { get; set; }

        public string Token { get; set; }        
    }
}