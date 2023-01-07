using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BugTracker.Data;
using BugTracker.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BugTracker.Controllers
{

    public class BugsController : Controller
    {

        private readonly ApplicationDbContext _db;

        public BugsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Bugs> objCategoryList = _db.bugs;
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            var model = new Bugs();
            model.Priority = "High";
            model.CreatedDateTime = DateTime.Today;
            return View(model);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Bugs obj)
        {
            if (ModelState.IsValid)
            {
                _db.bugs.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Ticket successfully Created";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var bugFromDb = _db.bugs.Find(id);

            if (bugFromDb == null)
            {
                return NotFound();
            }

            return View(bugFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Bugs obj)
        {
            if (ModelState.IsValid)
            {
                _db.bugs.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Ticket successfully Updated";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var bugFromDb = _db.bugs.Find(id);

            if (bugFromDb == null)
            {
                return NotFound();
            }

            return View(bugFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.bugs.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.bugs.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Ticket successfully Deleted";
            return RedirectToAction("Index");


        }
    }
}

