using eticket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Data.Services
{
    public class ProducerService : IProducerService
    {
        private readonly AppDbContext db;
        public ProducerService(AppDbContext context)
        {
            db = context;
        }
        public void Add(producer values)
        {
            db.producers.Add(values);
            db.SaveChanges();
        }

        public void Delete(int Id)
        {
            var obj=db.producers.Find(Id);
            db.producers.Remove(obj);
            db.SaveChanges();
        }

        public producer GetbyId(int Id)
        {
            var obj=db.producers.Find(Id);
            return obj;
        }

        public IEnumerable<producer> Getrec()
        {
           var li= db.producers.ToList();
            return li;
        }

        public void Update(int Id, producer values)
        {
            db.Update(values);
            db.SaveChanges();
        }
    }
}
