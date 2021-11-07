using eticket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Data.Services
{
    public interface IActorsService
    {
        IEnumerable<Actor> Getrec();

        Actor GetbyId(int Id);
        void Add(Actor actor);
        void Update(int Id, Actor actor);
        void Delete(int Id);
    }
}
