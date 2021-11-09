using eticket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Data.Services
{
    public interface IProducerService
    {
        IEnumerable<producer> Getrec();

        producer GetbyId(int Id);
        void Add(producer values);
        void Update(int Id, producer values);
        void Delete(int Id);
    }
}
