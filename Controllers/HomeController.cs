using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrueMatch.Models;
using TrueMatch.Models.Data;

namespace TrueMatch.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Success_Stories()
    {
        return View();
    } 
    public IActionResult AboutUs()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ContactUs()
    {
        var userEmail = HttpContext.Session.GetString("Email");
        if (userEmail == null)
        {
            return RedirectToAction("Login", "UserAuth");
        }
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ContactUs(Complain complain)
    {
        var userEmail = HttpContext.Session.GetString("Email");

        if (string.IsNullOrEmpty(userEmail))
        {
            _logger.LogWarning("No email found in session - redirecting to login");
            return RedirectToAction("Login", "UserAuth");
        }

        // Verify the email exists in Accounts table
        var accountExists = await _context.Accounts.AnyAsync(a => a.Email == userEmail);
        if (!accountExists)
        {
            _logger.LogWarning($"Email {userEmail} not found in Accounts table");
            return RedirectToAction("Login", "UserAuth");
        }

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Model validation failed");
            return View(complain);
        }

        try
        {
            complain.Email = userEmail; // Ensure email is set from session
            complain.ComplainId = Guid.NewGuid();
            complain.CreatedAt = DateTime.UtcNow;

            _context.Complains.Add(complain);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Your message has been sent.";
            return RedirectToAction("ContactUs");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving complaint");
            ModelState.AddModelError("", "An error occurred while saving your message. Please try again.");
            return View(complain);
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
