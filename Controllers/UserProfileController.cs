using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrueMatch.Models;
using TrueMatch.Models.Data;

namespace TrueMatch.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserProfileController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Profile()
        {
            var userEmail = HttpContext.Session.GetString("Email");
            if (userEmail == null)
            {
                return RedirectToAction("Login", "UserAuth");
            }

            var user = _context.Accounts.FirstOrDefault(u => u.Email == userEmail);
            return View(user);
        }
        // GET: Show the form
        public IActionResult UpdateProfile()
        {
            string email = HttpContext.Session.GetString("Email");

            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "UserAuth");

            var user = _context.Accounts.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return RedirectToAction("SignUp", "UserAuth");

            }

            return View(user);
        }


        // POST: Handle the form
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(Account model, IFormFile ProfileImage, IFormFile BackgroundImage)
        {
            string email = HttpContext.Session.GetString("Email"); // Get email again
            if (email == null) return RedirectToAction("Login", "Account");

            var user = _context.Accounts.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                // Update text fields
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Birthday = model.Birthday;
                user.Gender = model.Gender;
                user.City = model.City;
                user.Address = model.Address;
                user.Age = model.Age;
                user.AboutUser = model.AboutUser;

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                Directory.CreateDirectory(uploadsFolder);

                if (ProfileImage != null && ProfileImage.Length > 0)
                {
                    string profileFile = Guid.NewGuid() + Path.GetExtension(ProfileImage.FileName);
                    string profilePath = Path.Combine(uploadsFolder, profileFile);
                    using var stream = new FileStream(profilePath, FileMode.Create);
                    await ProfileImage.CopyToAsync(stream);
                    user.ProfileImageUrl = "/images/" + profileFile;
                }

                if (BackgroundImage != null && BackgroundImage.Length > 0)
                {
                    string bgFile = Guid.NewGuid() + Path.GetExtension(BackgroundImage.FileName);
                    string bgPath = Path.Combine(uploadsFolder, bgFile);
                    using var stream = new FileStream(bgPath, FileMode.Create);
                    await BackgroundImage.CopyToAsync(stream);
                    user.BackGroundImageUrl = "/images/" + bgFile;
                }

                _context.SaveChanges();
            }

            return RedirectToAction("Profile");
        }
        [HttpGet]
        public async Task <IActionResult> FindPartner(string gender,int ageStart, int ageEnd, string city)
        {
            string email = HttpContext.Session.GetString("Email"); // Get email again
            if (email == null) return RedirectToAction("Login", "Account");

            var partner = await _context.Accounts.Where(b => b.Gender == gender && b.City == city && b.Age>=ageStart && b.Age <= ageEnd).ToListAsync();
            if (partner.Any())
            {
                return View("FindPartner", partner);
            }
            else
            {
                ViewBag.Message = "No User found from these range";
                return View("FindPartner", new List<Account>());
            }
        }
        [HttpGet]
        public IActionResult FindPartnerProfile()
        {
            return View();
        }

    }
}
