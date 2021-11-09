using eticket.Data;
using eticket.Data.Services;
using eticket.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemaService dal;
        public CinemasController(ICinemaService service)
        {
            dal = service;
        }
        public IActionResult Index()
        {
            var allcinemas = dal.Getrec();
            return View(allcinemas);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("Logo,Name,Description")] Cinema values)//binding data with model and validating
        {
            if (!ModelState.IsValid)
            {
                return View(values);
            }
            dal.Add(values);
            return RedirectToAction("Create");
        }
        public IActionResult Edit(int Id)
        {
            var obj = dal.GetbyId(Id);
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,Logo,Name,Description")] Cinema values)
        {

            dal.Update(id, values);
            return RedirectToAction("Index");

        }
        public IActionResult Details(int id)
        {
            var obj = dal.GetbyId(id);
            return View(obj);
        }
        public IActionResult Delete(int id)
        {
            var obj = dal.GetbyId(id);
            return View(obj);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var actorDetails = dal.GetbyId(id);
            if (actorDetails == null) return View("NotFound");

            dal.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
