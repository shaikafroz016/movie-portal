using eticket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Data.ViewModel
{
    public class NewMovieDropdownsVM
    {
        public NewMovieDropdownsVM()
        {
            producers = new List<producer>();
            Cinemas = new List<Cinema>();
            Actors = new List<Actor>();
        }

        public List<producer> producers { get; set; }
        public List<Cinema> Cinemas { get; set; }
        public List<Actor> Actors { get; set; }
    }

}
