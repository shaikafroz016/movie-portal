using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Data.ViewModel
{
    public class RegisterVM
    {
        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage ="User Name is required")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Enter User name between 6 to 50 character")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50,MinimumLength =6,ErrorMessage ="Enter password between 6 to 50 character")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).{8}$",
         ErrorMessage = "Password must start with capital letter and catain atleast one special character")]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
