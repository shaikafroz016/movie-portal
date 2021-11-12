using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Models
{
    public class ApplicationUser:IdentityUser   //Identityuser is a microsoft defined class for authentication and authorization
    {
        [Display( Name = "Full Name")]
        public string Fullname { get; set; }
    }
}
