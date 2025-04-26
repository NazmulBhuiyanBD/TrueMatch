using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TrueMatch.Models;
using TrueMatch.Models.Data;

namespace TrueMatch.Controllers
{
    public class UserAuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserAuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /UserAuth/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /UserAuth/Login
        [HttpPost]
        public IActionResult Login(IFormCollection form)
        {
            var email = form["Email"].ToString();
            var password = form["Password"].ToString();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.LoginError = "Please provide email and password.";
                return View();
            }

            var existingUser = _context.Accounts
                .FirstOrDefault(a => a.Email == email && a.Password == password);

            if (existingUser != null)
            {
                HttpContext.Session.SetString("Email", existingUser.Email);
                HttpContext.Session.SetString("UserName", existingUser.UserName);
                return RedirectToAction("Profile", "UserProfile");
            }

            ViewBag.LoginError = "Invalid email or password.";
            return View();
        }



        // GET: /UserAuth/SignUp
        public IActionResult SignUp()
        {
            return View();
        }

        // POST: /UserAuth/SignUp
        [HttpPost]
        public IActionResult SignUp(IFormCollection form)
        {
            var userName = form["UserName"].ToString();
            var email = form["Email"].ToString();
            var password = form["Password"].ToString();
            var confirmPassword = form["ConfirmPassword"].ToString();

            if (password != confirmPassword)
            {
                ViewBag.SignUpError = "Passwords do not match.";
                return View();
            }

            var existingEmail = _context.Accounts.Any(a => a.Email == email);
            if (existingEmail)
            {
                ViewBag.SignUpError = "Email is already in use.";
                return View();
            }

            var account = new Account
            {
                UserName = userName,
                Email = email,
                Password = password
            };

            _context.Accounts.Add(account);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }
        public IActionResult Logout()
        {
            // Clear the session
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "UserAuth");
        }



    }
}
