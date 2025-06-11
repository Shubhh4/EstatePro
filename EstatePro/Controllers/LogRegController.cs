using EstatePro.Models;
using EstatePro.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EstatePro.Controllers
{
    public class LogRegController : Controller
    {
        LogRegRepo lrrepo;
        public LogRegController(LogRegRepo lrrepo) 
        { 
            this.lrrepo = lrrepo;
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.RoleList = Enum.GetValues(typeof(UserRole))
                        .Cast<UserRole>()
                        .Where(r => r != UserRole.Admin)
                        .Select(r => new SelectListItem
                        {
                            Text = r.ToString(),
                            Value = r.ToString()
                        }).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (lrrepo.EmailExists(user.Email))
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return View(user);
            }

            if (ModelState.IsValid)
            {
                lrrepo.Register(user);
                return RedirectToAction("Login");
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = lrrepo.Login(email, password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("UserRole", user.Role.ToString());

                // Redirect to role-based dashboard
                switch (user.Role)
                {
                    case UserRole.Admin:
                        return RedirectToAction("Index", "Dashboard");
                    case UserRole.Agent:
                        return RedirectToAction("Index", "Dashboard");
                    case UserRole.Seller:
                        return RedirectToAction("Index", "Dashboard");
                    case UserRole.Buyer:
                        return RedirectToAction("Index", "UserDashboard");
                    case UserRole.Tenant:
                        return RedirectToAction("Index", "UserDashboard");
                    default:
                        return RedirectToAction("Login");
                }
            }

            ViewBag.Error = "Invalid credentials.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
