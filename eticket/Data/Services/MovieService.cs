using eticket.Data.ViewModel;
using eticket.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Data.Services
{
    public class MovieService : IMoviesService
    {
        private readonly AppDbContext db;
        public MovieService(AppDbContext context)
        {
            db = context;
        }
        public void Add(Movie values)
        {
            db.Movies.Add(values);
            db.SaveChanges();
        }

        public void Delete(int Id)
        {
            var obj=db.Movies.Find(Id);
            db.Movies.Remove(obj);
            db.SaveChanges();
        }

        public Task<Movie> GetbyId(int Id)
        {
            var obj = db.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am=>am.Actor_Movies).ThenInclude(a=>a.Actor)
                .FirstOrDefaultAsync(n => n.Id == Id);
            return obj;
        }

        public NewMovieDropdownsVM getMovieDropdownList()
        {
            var response = new NewMovieDropdownsVM();
            response.Actors = db.Actors.ToList();
            response.Cinemas = db.Cinemas.OrderBy(e => e.Name).ToList();
            response.producers = db.producers.ToList();
            return response;
        }

        public IEnumerable<Movie> Getrec()
        {
            var li = db.Movies.Include(e => e.Cinema).ToList();
            return li;
        }

        public void Update(int Id, Movie values)
        {
            db.Update(values);
            db.SaveChanges();
        }
    }
}
