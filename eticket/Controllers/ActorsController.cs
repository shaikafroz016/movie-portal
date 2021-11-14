using eticket.Data;
using eticket.Data.Services;
using eticket.Data.Static;
using eticket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eticket.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ActorsController : Controller
    {
        private readonly IActorsService dal;
        public ActorsController(IActorsService service)
        {
            dal = service;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            var data = dal.Getrec();
            return View(data);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create([Bind("profilepic,fullname,bio")]Actor actor)//binding data with model and validating
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            dal.Add(actor);
            return RedirectToAction("Create");
        }
        public IActionResult Edit(int Id)
        {
            var obj = dal.GetbyId(Id);
            if (obj == null)
            {
                return View("NotFound");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(int id,[Bind("Id,profilepic,fullname,bio")] Actor actor)
        {
            
            dal.Update(id,actor);
            return RedirectToAction("Index");

        }
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
           var obj= dal.GetbyId(id);
            if (obj == null)
            {
                return View("NotFound");
            }
            return View(obj);
        }
        public IActionResult Delete(int id)
        {
            var obj = dal.GetbyId(id);
            if (obj == null)
            {
                return View("NotFound");
            }
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
