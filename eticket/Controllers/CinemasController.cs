using eticket.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Controllers
{
    public class CinemasController : Controller
    {
        private readonly AppDbContext db;
        public CinemasController(AppDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var allcinemas = db.Cinemas.ToList();
            return View(allcinemas);
        }
    }
}
