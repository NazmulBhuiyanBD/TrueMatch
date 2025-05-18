using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrueMatch.Models;
using TrueMatch.Models.Data;
using TrueMatch.Models.ViewModels;

namespace TrueMatch.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> UserList(string searchString)
        {
            var username = HttpContext.Session.GetString("AdminUser");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account", new { area = "Admin" });
            }

            var accounts = _context.Accounts.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                accounts = accounts.Where(a =>
                    a.UserName.Contains(searchString) ||
                    a.Email.Contains(searchString) ||
                    (a.FirstName != null && a.FirstName.Contains(searchString)) ||
                    (a.LastName != null && a.LastName.Contains(searchString)));
            }

            var viewModel = new AccountListViewModel
            {
                Accounts = await accounts.AsNoTracking().ToListAsync(),
                CurrentFilter = searchString
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(string id, bool status)
        {
            var account = await _context.Accounts.FindAsync(id); // Now it's a string
            if (account == null)
            {
                return NotFound();
            }

            account.status = status;
            _context.Update(account);
            await _context.SaveChangesAsync();

            TempData["Success"] = "User status updated successfully.";

            return RedirectToAction("UserList");
        }

        public async Task<IActionResult> Complain()
        {
            var username = HttpContext.Session.GetString("AdminUser");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account", new { area = "Admin" });
            }

            var complaints = await _context.Complains
                                           .OrderByDescending(c => c.CreatedAt)
                                           .ToListAsync();

            return View(complaints);
        }
    }
}
