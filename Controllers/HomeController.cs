using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BugTracker.Models;
using Microsoft.AspNetCore.Authorization;
using BugTracker.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugTracker.Controllers;

[Authorize]
public class HomeController : Controller
{

    private readonly ApplicationDbContext _db;

    public HomeController(ApplicationDbContext db)
    {
        _db = db;
    }

    //private readonly ILogger<HomeController> _logger;

    //public HomeController(ILogger<HomeController> logger)
    //{
    //    _logger = logger;
    //}

    public IActionResult Index()
    {
        
        IEnumerable<Projects> objCategoryList = _db.projects;
        
        return View(objCategoryList);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    //GET
    public IActionResult Create()
    {
        var Contributorss = _db.Users.ToList();
        List<SelectListItem> contributorss = new List<SelectListItem>();

        var model = new Projects();

        foreach (var item in Contributorss)
        {
            contributorss.Add(new SelectListItem
            {
                Text = item.UserName,
                Value = item.UserName?.ToString()
            });
        }

        model.Contributorss = contributorss;
        
        return View(model);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Projects obj, string SelectedValues)
    {
        if (ModelState.IsValid)
        {   
            if (SelectedValues.Contains(','))
            {
                obj.Contributors = null;
                var values = SelectedValues.Split(',');
                foreach(var value in values)
                {
                    obj.Contributors = obj.Contributors + value + "\r\n";
                }
            }
            else
            {
                obj.Contributors = SelectedValues;
            }
            

            _db.projects.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Project successfully Added";
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
        var projectFromDb = _db.projects.Find(id);

        if (projectFromDb == null)
        {
            return NotFound();
        }

        var Contributorss = _db.Users.ToList();
        List<SelectListItem> contributorss = new List<SelectListItem>();
        foreach (var item in Contributorss)
        {
            contributorss.Add(new SelectListItem
            {
                Text = item.UserName,
                Value = item.UserName?.ToString()
            });
        }

        projectFromDb.Contributorss = contributorss;

        return View(projectFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Projects obj, string SelectedValues)
    {
        if (ModelState.IsValid)
        {
            if (SelectedValues.Contains(','))
            {
                obj.Contributors = null;
                var values = SelectedValues.Split(',');
                foreach (var value in values)
                {
                    obj.Contributors = obj.Contributors + value + "\r\n";
                }
            }
            else
            {
                obj.Contributors = SelectedValues;
            }

            _db.projects.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Project successfully Updated";
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
        var projectFromDb = _db.projects.Find(id);

        if (projectFromDb == null)
        {
            return NotFound();
        }

        var Contributorss = _db.Users.ToList();
        List<SelectListItem> contributorss = new List<SelectListItem>();
        foreach (var item in Contributorss)
        {
            contributorss.Add(new SelectListItem
            {
                Text = item.UserName,
                Value = item.UserName?.ToString()
            });
        }

        projectFromDb.Contributorss = contributorss;

        return View(projectFromDb);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _db.projects.Find(id);
        if (obj == null)
        {
            return NotFound();
        }

        _db.projects.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Project successfully Deleted";
        return RedirectToAction("Index");


    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

