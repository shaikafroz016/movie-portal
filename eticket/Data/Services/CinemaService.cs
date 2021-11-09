using eticket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Data.Services
{
    public class CinemaService : ICinemaService
    {
        private readonly AppDbContext db;
        public CinemaService(AppDbContext context)
        {
            db = context;
        }
        public void Add(Cinema values)
        {
            db.Cinemas.Add(values);
            db.SaveChanges();
        }

        public void Delete(int Id)
        {
            var obj = db.Cinemas.Find(Id);
            db.Cinemas.Remove(obj);
            db.SaveChanges();
        }

        public Cinema GetbyId(int Id)
        {
            var obj = db.Cinemas.Find(Id);
            return obj;
        }

        public IEnumerable<Cinema> Getrec()
        {
            var obj = db.Cinemas.ToList();
            return obj;
        }

        public void Update(int Id, Cinema values)
        {
            db.Update(values);
            db.SaveChanges();
        }
    }
}
