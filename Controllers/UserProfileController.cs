using Microsoft.AspNetCore.Mvc;

namespace TrueMatch.Controllers
{
    public class UserProfileController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
    }
}
