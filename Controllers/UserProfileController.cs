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
                var today = DateTime.Today;
                var birthDate = model.Birthday.Value;
              
                user.Age = today.Year - birthDate.Year;
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
        public async Task<IActionResult> FindPartnerProfile(string email)
        {
            if (string.IsNullOrEmpty(email)) return BadRequest();

            var partner = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email);
            if (partner == null) return NotFound();

            return View(partner);
        }
        public IActionResult UserPost()
        {
            string email = HttpContext.Session.GetString("Email");
            if (email == null) return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserPost(Post p)
        {
            string? email = HttpContext.Session.GetString("Email");
            if (email == null)
                return RedirectToAction("Login", "Account");

            p.Email = email;

            // Handle image upload
            if (p.ImageFile != null && p.ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(p.ImageFile.FileName);
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await p.ImageFile.CopyToAsync(stream);
                }

                p.ImageUrl = "/images/" + fileName;
            }

            _context.Posts.Add(p);
            await _context.SaveChangesAsync();

            return RedirectToAction("Profile"); // Or wherever you want to go after saving
        }
        [HttpGet]
        public IActionResult ShowPost()
        {
            var userEmail = HttpContext.Session.GetString("Email");
            if (userEmail == null)
            {
                return RedirectToAction("Login", "UserAuth");
            }
            var posts = _context.Posts
                .Include(p => p.Account) // Include the Account navigation property
                .OrderByDescending(p => p.Id) // Newest first
                .ToList();

            return View(posts);
        }



    }
}
