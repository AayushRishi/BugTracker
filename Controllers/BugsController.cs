using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BugTracker.Controllers
{
    [Authorize]
    public class BugsController : Controller
    {

        private readonly ApplicationDbContext _db;

        public BugsController(ApplicationDbContext db)
        {
            _db = db;
        }

        //[Authorize(Roles = "User")]
        public IActionResult Index()
        {
            IEnumerable<Bugs> objCategoryList = _db.bugs;
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            List<SelectListItem> projectslist = new List<SelectListItem>();
            var projectlist = _db.projects;

            foreach(var pj in projectlist)
            {
                projectslist.Add(new SelectListItem
                {
                    Text = pj.Project,
                    Value = pj.Project?.ToString()
                });
            }

            var model = new Bugs();
            model.Priority = "High";
            model.CreatedDateTime = DateTime.Today;
            model.Projects = projectslist;
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

            List<SelectListItem> projectslist = new List<SelectListItem>();
            var projectlist = _db.projects;

            foreach (var pj in projectlist)
            {
                projectslist.Add(new SelectListItem
                {
                    Text = pj.Project,
                    Value = pj.Project?.ToString()
                });
            }

            bugFromDb.Projects = projectslist;
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

            List<SelectListItem> projectslist = new List<SelectListItem>();
            var projectlist = _db.projects;

            foreach (var pj in projectlist)
            {
                projectslist.Add(new SelectListItem
                {
                    Text = pj.Project,
                    Value = pj.Project?.ToString()
                });
            }

            bugFromDb.Projects = projectslist;

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

