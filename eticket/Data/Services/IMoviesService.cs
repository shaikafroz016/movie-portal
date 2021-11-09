using eticket.Data.ViewModel;
using eticket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Data.Services
{
    public interface IMoviesService
    {
        IEnumerable<Movie> Getrec();

        Task<Movie> GetbyId(int Id);
        void Add(Movie values);
        void Update(int Id, Movie values);
        void Delete(int Id);
        NewMovieDropdownsVM getMovieDropdownList();
    }
}
