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
    [Authorize(Roles =UserRoles.Admin)]
    public class ProducerController : Controller
    {
        private readonly IProducerService dal;
        public ProducerController(IProducerService ser)
        {
            dal = ser;
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
        public IActionResult Create([Bind("profilepic,fullname,bio")] producer values)//binding data with model and validating
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
        public IActionResult Edit(int id, [Bind("Id,profilepic,fullname,bio")] producer values)
        {

            dal.Update(id, values);
            return RedirectToAction("Index");

        }
        [AllowAnonymous]
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
