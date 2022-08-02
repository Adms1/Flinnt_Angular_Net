using System.ComponentModel.DataAnnotations;

namespace Flinnt.Business.ViewModels
{
    public class AccountModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }        
    }
}