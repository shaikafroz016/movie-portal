using eticket.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Data.Services
{
    public class ActorsService : IActorsService
    {
        private readonly AppDbContext db;
        public ActorsService(AppDbContext context)
        {
            db = context;
        }
        public void Add(Actor actor)
        {
            db.Actors.Add(actor);
            db.SaveChanges();
        }

        public void Delete(int Id)
        {
            var obj = db.Actors.Find(Id);
            db.Actors.Remove(obj);
            db.SaveChanges();
        }

        public Actor GetbyId(int Id)
        {
            var obj = db.Actors.Find(Id);
            return obj;
        }

        public IEnumerable<Actor> Getrec()
        {
            var rec= db.Actors.ToList();
            return rec;
        }

        public void Update(int Id, Actor actor)
        {
            db.Update(actor);
            db.SaveChanges();
            
        }
    }
}
