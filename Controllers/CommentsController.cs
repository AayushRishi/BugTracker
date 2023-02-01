using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BugTracker.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CommentsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: /<controller>/
        public IActionResult Index(int? id)
        {
            var model = new Comments();

            var bugFromDb = _db.bugs.Find(id);
            model.hashmap_tDetail = new List<KeyValuePair<string, string>>();
            model.hashmap_tDetail.Add(new KeyValuePair<string, string>("Project", bugFromDb.Project));
            model.hashmap_tDetail.Add(new KeyValuePair<string, string>("Ticket", bugFromDb.Ticket));
            model.hashmap_tDetail.Add(new KeyValuePair<string, string>("Status", bugFromDb.Status));
            model.hashmap_tDetail.Add(new KeyValuePair<string, string>("Priority", bugFromDb.Priority));


            model.BugId = id.ToString();

            if (id == null || id == 0)
            {
                return NotFound();
            }

            
            var ticketFromDb = _db.comments.ToList();
            model.ticketList = new List<string>();

            model.hashmap = new List<KeyValuePair<string, string>>();


            if (ticketFromDb != null && ticketFromDb.Any())
            {
                foreach (var tckt in ticketFromDb)
                {
                    int abc;
                    abc = int.Parse(tckt.BugId);

                    if (abc == id)
                    {

                        model.hashmap.Add(new KeyValuePair<string, string>(tckt.Comment.ToString(), tckt.CreatedByUser.ToString()));

                        //if (tckt.Comment != null)
                        //{
                        //    model.ticketList.Add(tckt.Comment.ToString());
                        //}

                    }
                }
            }



            
            //if (model.ticketList.Count == 0)
            //{
            //    //return NotFound();
            //    //TempData["Bugid"] = id;
            //    //TempData["ModelIsNull"] = true;
                
            //    return View(model);
            //}

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Comments obj)
        {

            if (ModelState.IsValid)
            {
                obj.Id = 0;
                var user = User.Identity.Name;
                obj.CreatedByUser = user;

                _db.comments.Add(obj);
                
                _db.SaveChanges();

                TempData["success"] = "Comment successfully added";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Comments obj)
        {
            int id = int.Parse(obj.BugId);
            var recordsToDelete = _db.comments.Where(x => x.BugId == obj.BugId);

            _db.comments.RemoveRange(recordsToDelete);
            _db.SaveChanges();
            TempData["success"] = "Comments successfully cleared";
            return RedirectToAction("Index", new { id = id });

        }
    }
}

