using eticket.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Controllers
{
    public class ProducerController : Controller
    {
        private readonly AppDbContext db;
        public ProducerController(AppDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var allproducers = db.producers.ToList();
            return View(allproducers);
        }
    }
}
