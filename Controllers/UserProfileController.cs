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
        public async Task<IActionResult> FindPartnerProfile(string email)
        {
            if (string.IsNullOrEmpty(email)) return BadRequest();

            var partner = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email);
            if (partner == null) return NotFound();

            return View(partner);
        }

        [HttpPost]
        public IActionResult SendFriendRequest(string receiverEmail)
        {
            var senderEmail = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(senderEmail)) return RedirectToAction("Login", "UserAuth");

            if (_context.FriendRequests.Any(f =>
                (f.SenderEmail == senderEmail && f.ReceiverEmail == receiverEmail) ||
                (f.SenderEmail == receiverEmail && f.ReceiverEmail == senderEmail)))
            {
                TempData["Message"] = "Friend request already exists.";
                return RedirectToAction("FindPartner");
            }

            var request = new FriendRequest
            {
                SenderEmail = senderEmail,
                ReceiverEmail = receiverEmail
            };

            _context.FriendRequests.Add(request);
            _context.SaveChanges();

            TempData["Message"] = "Friend request sent!";
            return RedirectToAction("FindPartner");
        }

        public IActionResult FriendRequests()
        {
            var userEmail = HttpContext.Session.GetString("Email");
            var requests = _context.FriendRequests
                .Where(fr => fr.ReceiverEmail == userEmail && !fr.IsAccepted)
                .ToList();

            return View(requests);
        }

        [HttpPost]
        public IActionResult AcceptFriend(int id)
        {
            var request = _context.FriendRequests.Find(id);
            if (request != null)
            {
                request.IsAccepted = true;
                _context.SaveChanges();
            }
            return RedirectToAction("FriendRequests");
        }

        public IActionResult FriendList()
        {
            var userEmail = HttpContext.Session.GetString("Email");

            var friends = _context.FriendRequests
                .Where(fr => (fr.SenderEmail == userEmail || fr.ReceiverEmail == userEmail) && fr.IsAccepted)
                .ToList();

            var friendAccounts = friends.Select(f =>
                f.SenderEmail == userEmail
                    ? _context.Accounts.FirstOrDefault(a => a.Email == f.ReceiverEmail)
                    : _context.Accounts.FirstOrDefault(a => a.Email == f.SenderEmail)).ToList();

            return View(friendAccounts);
        }


    }
}
