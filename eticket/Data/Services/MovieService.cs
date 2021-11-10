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
        public void Add(NewMovieVM values)
        {
            var newmovie = new Movie()
            {
                Name = values.Name,
                Description = values.Description,
                price = values.price,
                ImageUrl = values.ImageUrl,
                CinemaId = values.CinemaId,
                startdate = values.startdate,
                enddate = values.enddate,
                MovieCatg = values.MovieCatg,
                ProducerId = values.ProducerId
            };
            db.Movies.Add(newmovie);
            db.SaveChanges();
            //adding actor and movie id in actormovie table
            foreach(var actorid in values.ActorIds)
            {
                var actormov = new Actor_Movie()
                {
                    ActorId = actorid,
                    MovieId = newmovie.Id
                };
                db.Actors_Movies.Add(actormov);
            }
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

        public async Task<NewMovieDropdownsVM> getMovieDropdownList()
        {
            var response = new NewMovieDropdownsVM()
            {
                Actors = await db.Actors.OrderBy(n => n.fullname).ToListAsync(),
                Cinemas = await db.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                producers = await db.producers.OrderBy(n => n.fullname).ToListAsync()
            };

            return response;
        }

        public IEnumerable<Movie> Getrec()
        {
            var li = db.Movies.Include(e => e.Cinema).ToList();
            return li;
        }

        public async Task Update(NewMovieVM data)
        {
            var dbMovie = await db.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbMovie != null)
            {
                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.price = data.price;
                dbMovie.ImageUrl = data.ImageUrl;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.startdate = data.startdate;
                dbMovie.enddate = data.enddate;
                dbMovie.MovieCatg = data.MovieCatg;
                dbMovie.ProducerId = data.ProducerId;
                await db.SaveChangesAsync();
            }

            //Remove existing actors
            var existingActorsDb = db.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
            db.Actors_Movies.RemoveRange(existingActorsDb);
            await db.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await db.Actors_Movies.AddAsync(newActorMovie);
            }
            await db.SaveChangesAsync();
        }
    }
}
