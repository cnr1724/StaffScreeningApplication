using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StaffScreeningApp.Models;

namespace StaffScreeningApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IdentityModel db;
        public HomeController(ILogger<HomeController> logger, IdentityModel context)
        {
            _logger = logger;
            db = context;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(Login obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (db.login.Any(a => a.Email == obj.Email && a.Password == obj.Password))
                    {
                        TempData["user"] = obj.Email;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error = "!!Please Enter valid login credentials.";
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ViewBag.error = "!!There is some error.";
            }
            return View();
        }
        public ActionResult Registration(Login obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (db.login.Any(a => a.Email == obj.Email))
                    {
                        ViewBag.error = " Email Id already exists.Please try with new email id.";
                    }
                    else
                    {
                        db.login.Add(obj);
                        db.SaveChanges();
                        ViewBag.success = "Sign Up successfully.";
                        return View();
                    }
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.error = "!!There is some error.";
            }
            return View();
        }
        public ActionResult SaveModel(Staffscreening model)
        {
            string? email = TempData["user"] as string;
             var userLogin = db.login.FirstOrDefault(a => a.Email == email);

                if (userLogin != null)
                {
                    model.user_id = userLogin.Id;
                    string selectedFeverchk = model.fever_check;
                    string selectedRunnynosechk = model.runny_nose_check;
                    string selectedSorethroatchk = model.sore_throat_check;
                    if (selectedFeverchk == "Yes" || selectedRunnynosechk == "Yes" || selectedSorethroatchk == "Yes")
                    {
                        db.Staffscreenings.Add(model);
                        db.SaveChanges();
                        return RedirectToAction("FailScreen");
                    }
                    else if (selectedFeverchk == "No" && selectedRunnynosechk == "No" && selectedSorethroatchk == "No")
                    {
                        db.Staffscreenings.Add(model);
                        db.SaveChanges();
                        return RedirectToAction("PassScreen");
                    }                
            }
            return View();
        }
        public ActionResult FailScreen()
        {
            return View();
        }
        public ActionResult PassScreen()
        {
            return View();
        }
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}