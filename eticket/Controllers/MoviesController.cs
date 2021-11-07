using eticket.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbContext db;
        public MoviesController(AppDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var allmovies = db.Movies.Include(e => e.Cinema).ToList();
            return View(allmovies);
        }
    }
}
