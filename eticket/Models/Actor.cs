using eticket.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eticket.Models
{
    public class Actor
    {
        [Key]
        public int  Id { get; set; }
        [Display(Name ="Profile Picture ")]
        [Required (ErrorMessage = "Profie Pic is requerd")]
        public string profilepic { get; set; }
        [Required (ErrorMessage ="Name is required")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="Length must be in 3 to 50 char")]
        [Display(Name ="Full Name")]
        public string fullname  { get; set; }
        [Required (ErrorMessage ="Required")]
        public string bio  { get; set; }
        //Relatio
        public List<Actor_Movie> Actor_Movies { get; set; }
    }
}