using eticket.Data;
using eticket.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService dal;
        public MoviesController(IMoviesService service)
        {
            dal = service;
        }
        public IActionResult Index()
        {
            var allmovies = dal.Getrec();
            return View(allmovies);
        }
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await dal.GetbyId(id);
            return View(movieDetail);
        }

        //GET: Movies/Create
        public  IActionResult Create()
        {
            var movieDropdownsData = dal.getMovieDropdownList();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View();
        }

    //    [HttpPost]
    //    public  Task<IActionResult> Create(NewMovieVM movie)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            var movieDropdownsData = await _service.GetNewMovieDropdownsValues();

    //            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
    //            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
    //            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

    //            return View(movie);
    //        }

    //        await _service.AddNewMovieAsync(movie);
    //        return RedirectToAction(nameof(Index));
    //    }
    }
}
