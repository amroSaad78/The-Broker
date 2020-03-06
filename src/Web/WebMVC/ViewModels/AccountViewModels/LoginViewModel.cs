using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Required")]
        [EmailAddress(ErrorMessage = "Invalid")]
        [MaxLength(30)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [MaxLength(15)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
