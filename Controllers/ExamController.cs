using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using exam.Models;

namespace exam.Controllers
{
    public class ExamController : Controller
    {
        private MyContext _db;
        private int? uid
        {
            get { return HttpContext.Session.GetInt32("UserId"); }
        }
        private bool isLoggedIn
        {
            get { return uid != null; }
        }


        public ExamController(MyContext context)
        {
            _db = context;
        }

//*********************************** function display dashboardn (we send all wedding list and loggedin user)

        [HttpGet("home")]
        public IActionResult Dashboard()
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Activites> allActivites = _db
                .Activitess   
                .Include(a => a.CreatedBy) 
                .Include(a => a.CommingUsers)
                .OrderBy(allActivites =>allActivites.Date)
                .ThenBy(allActivites =>allActivites.Time)
                .ToList();
            User u = _db.Users.FirstOrDefault(u => u.UserId == (int)uid);
            ViewBag.User = u;
            return View(allActivites); 
        }

        [HttpGet("new")]
        public IActionResult New()  
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            User u = _db.Users.FirstOrDefault(u => u.UserId == (int)uid);
            ViewBag.User = u;
            return View();
        }
        [HttpPost("postnew")]
        public IActionResult CreateActivity(Activites activity)
        {
            if (activity.Date < DateTime.Now)
            {
                ModelState.AddModelError("Date", "Activity Date must be in the futuer");
            }
            if (ModelState.IsValid)
            {
                activity.UserId = (int)uid;
                _db.Activitess.Add(activity);
                _db.SaveChanges();
                return Redirect($"/activity/{activity.ActivitesId}");
            
            }
            User u = _db.Users.FirstOrDefault(u => u.UserId == (int)uid);
            ViewBag.User = u;
            return View("New");
        }
        [HttpGet("activity/{activityId}")]
        public IActionResult Activity(int activityID)
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            Activites thisActivity = _db
            .Activitess
            .Include(a => a.CreatedBy)
            .Include(a => a.CommingUsers)
            .ThenInclude(A => A.Gest)
            .FirstOrDefault(w=> w.ActivitesId == activityID);
            User u = _db.Users.FirstOrDefault(u => u.UserId == (int)uid);
            ViewBag.User = u;
            return View(thisActivity);
        }
        [HttpGet("delete/{activityId}")]  
        public IActionResult Delete(int activityID)
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            Activites deletedAct = _db.Activitess.FirstOrDefault(a => a.ActivitesId == activityID);
            _db.Activitess.Remove(deletedAct);
            _db.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet("Join/{activityId}")]
        public IActionResult Join(int activityID)
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            Patricipanats part = new Patricipanats();
            part.UserId = (int)uid;
            part.ActivitesId = activityID;
            _db.Patricipanatss.Add(part);
            _db.SaveChanges();
            return RedirectToAction("Dashboard");
        }


        [HttpGet("leave/{activityId}")]
        public IActionResult Leave(int activityID)
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            Patricipanats leave = _db.Patricipanatss.FirstOrDefault(l => l.GestOf.ActivitesId == activityID && l.Gest.UserId == (int)uid);
            _db.Patricipanatss.Remove(leave);
            _db.SaveChanges();
            return RedirectToAction("Dashboard");
        }



    }
}