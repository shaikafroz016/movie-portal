using eticket.Data;
using eticket.Data.Services;
using eticket.Data.Static;
using eticket.Data.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]

    public class MoviesController : Controller
    {
        private readonly IMoviesService dal;
        public MoviesController(IMoviesService service)
        {
            dal = service;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            var allmovies = dal.Getrec();
            return View(allmovies);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies =  dal.Getrec();
            if (!string.IsNullOrEmpty(searchString))
            {
                char[] a = searchString.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                searchString = new string(a);
                var filteredResult = allMovies.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                if (filteredResult.Count>0)
                {
                    return View("Index", filteredResult);
                }
                else
                {
                    return View("Index", allMovies);
                }
            }
            

            return View("Index", allMovies);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await dal.GetbyId(id);
            return View(movieDetail);
        }

        //GET: Movies/Create
        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await dal.getMovieDropdownList();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.producers, "Id", "fullname");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "fullname");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM values)
        {
            var movieDropdownsData = await dal.getMovieDropdownList();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.producers, "Id", "fullname");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "fullname");
            if (!ModelState.IsValid)
            {
                return View(values);
            }
            dal.Add(values);
            return RedirectToAction("Index");
        }

        //GET: Movies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await dal.GetbyId(id);
            if (movieDetails == null) return View("NotFound");

            var response = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                price = movieDetails.price,
                startdate = movieDetails.startdate,
                enddate = movieDetails.enddate,
                ImageUrl = movieDetails.ImageUrl,
                MovieCatg = movieDetails.MovieCatg,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actor_Movies.Select(n => n.ActorId).ToList(), //select statement used for selecting data inside the viewmodel
            };

            var movieDropdownsData = await dal.getMovieDropdownList();
            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.producers, "Id", "fullname");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "fullname");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {
            if (id != movie.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await dal.getMovieDropdownList();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.producers, "Id", "fullname");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "fullname");

                return View(movie);
            }

             await dal.Update(movie);
            return RedirectToAction(nameof(Index));
        }
    }
}

