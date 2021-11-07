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
        public string profilepic { get; set; }
        public string fullname { get; set; }
        public string bio { get; set; }
        //relationships
        public List<Movie> Movies { get; set; } //one-to-many relation
    }
}