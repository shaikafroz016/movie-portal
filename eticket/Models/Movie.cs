using eticket.Data;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eticket.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Name{ get; set; }
        public string Description { get; set; }
        public double price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public MovieCatg MovieCatg { get; set; }
        //relationship
        public List<Actor_Movie> Actor_Movies { get; set; }

        //cenimaprop    
        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")]
        public Cinema Cinema { get; set; }

        //producer
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public producer Producer { get; set; }

    }
}