using eticket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Data.Services
{
    public interface ICinemaService
    {
        IEnumerable<Cinema> Getrec();

        Cinema GetbyId(int Id);
        void Add(Cinema values);
        void Update(int Id, Cinema values);
        void Delete(int Id);
    }
}
