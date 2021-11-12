using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Data.ViewModel
{
    public class LoginVM
    {
        [Required (ErrorMessage ="Email is Required")]
        [Display(Name ="Email")]
        public string EmailAddress { get; set; }

        [Required (ErrorMessage ="Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
