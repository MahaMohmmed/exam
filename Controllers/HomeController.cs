using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace exam.Controllers
{
    public class HomeController : Controller
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


        public HomeController(MyContext context)
        {
            _db = context;
        }
        [HttpGet("")]
        public IActionResult first()
        {
            return RedirectToAction("Index");
        }
//*********************************** check if user logged in or not and show form
        [HttpGet("signin")]
        public IActionResult Index()
        {
            
            if (!isLoggedIn)
            {
                return View();
            }
            return RedirectToAction("Dashboard", "Exam");
        }
        [HttpPost("register")]
//*********************************** function Process Regester

        public IActionResult Register(User user)
        {
            
            if (ModelState.IsValid)
            {
                if (_db.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Index");
                }
                
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.Password = Hasher.HashPassword(user, user.Password);
                _db.Users.Add(user);
                _db.SaveChanges();
                HttpContext.Session.SetInt32("UserId", user.UserId);
                return RedirectToAction("Dashboard", "Exam");
            }
            return View("Index");
        }
//*********************************** function Process LogIn

        [HttpPost("letmein")]
        public IActionResult LetMeIn(LoginUser lu)
        {
            if (ModelState.IsValid)
            {
                User getUser = _db.Users.FirstOrDefault(u => u.Email == lu.LoginEmail);
                if (getUser == null)
                {
                    ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
                    return View("Index");
                }
                var hasher = new PasswordHasher<LoginUser>();
                var result = hasher.VerifyHashedPassword(lu, getUser.Password, lu.LoginPassword);
                if (result == 0) 
                {
                    ModelState.AddModelError("LoginPassword", "Invalid Email/Password");
                    return View("Index");
                }
                HttpContext.Session.SetInt32("UserId", getUser.UserId);
                return RedirectToAction("Dashboard", "Exam");
            }
            return View("Index");
        }
        [HttpGet("logout")]
//*********************************** function Process LogOut

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
// *****************************************************************************


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
