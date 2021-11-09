using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eticket.Models
{
    public class producer
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage ="Profile picture is required.")]
        public string profilepic { get; set; }
        [Required (ErrorMessage ="Full Name is Required")]
        [Display (Name ="Full Name")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="Please type name in between 3 to 50 char.")]
        public string fullname { get; set; }
        [Required]
        public string bio { get; set; }
        //relationships
        public List<NewMovieVM> Movies { get; set; } //one-to-many relation
    }
}