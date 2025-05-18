using Microsoft.AspNetCore.Mvc;

namespace TrueMatch.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("AdminUser");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account", new { area = "Admin" });
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AdminAccount model)
        {
            if (ModelState.IsValid)
            {
                if (model.UserName == "admin" && model.Password == "1234")
                {
                    HttpContext.Session.SetString("AdminUser", model.UserName);
                    return RedirectToAction("Index", "Account", new { area = "Admin" });
                }

                ModelState.AddModelError(string.Empty, "Invalid login credentials");
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AdminUser");
            TempData["Success"] = "You have been logged out successfully.";
            return RedirectToAction("Login", "Account", new { area = "Admin" });
        }



    }
}
