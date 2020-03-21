using System.ComponentModel.DataAnnotations;

namespace Identity.API.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(30)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(15)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}