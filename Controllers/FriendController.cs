using Microsoft.AspNetCore.Mvc;
using TrueMatch.Models;
using TrueMatch.Models.Data;

public class FriendController : Controller
{
    private readonly ApplicationDbContext _context;

    public FriendController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult SendFriendRequest(string receiverEmail)
    {
        var senderEmail = HttpContext.Session.GetString("Email");

        if (string.IsNullOrEmpty(senderEmail))
            return RedirectToAction("Login", "UserAuth");

        if (senderEmail == receiverEmail)
            return BadRequest("You cannot send a friend request to yourself.");

        // Check if request already exists
        bool alreadyRequested = _context.Friends.Any(f =>
            (f.RequesterEmail == senderEmail && f.ReceiverEmail == receiverEmail) ||
            (f.RequesterEmail == receiverEmail && f.ReceiverEmail == senderEmail));

        if (alreadyRequested)
            return Content("Friend request already sent or you are already friends.");

        var friendRequest = new Friend
        {
            RequesterEmail = senderEmail,
            ReceiverEmail = receiverEmail,
            RequestDate = DateTime.Now,
            IsAccepted = false
        };

        _context.Friends.Add(friendRequest);
        _context.SaveChanges();

        return RedirectToAction("Profile", "Account", new { email = receiverEmail });
    }


    [HttpPost]
    public IActionResult AcceptFriendRequest(int id)
    {
        var senderEmail = HttpContext.Session.GetString("Email");

        if (string.IsNullOrEmpty(senderEmail))
            return RedirectToAction("Login", "UserAuth");
        var request = _context.Friends.Find(id);
        if (request != null)
        {
            request.IsAccepted = true;
            _context.SaveChanges();
        }
        return RedirectToAction("FriendRequests");
    }

    public IActionResult FriendRequests()
    {
        var senderEmail = HttpContext.Session.GetString("Email");

        if (string.IsNullOrEmpty(senderEmail))
            return RedirectToAction("Login", "UserAuth");

        var myEmail = HttpContext.Session.GetString("Email");
        var requests = _context.Friends
            .Where(f => f.ReceiverEmail == myEmail && !f.IsAccepted)
            .ToList();

        return View(requests);
    }

    public IActionResult FriendList()
    {
        var myEmail = HttpContext.Session.GetString("Email");

        if (string.IsNullOrEmpty(myEmail))
        {
            return RedirectToAction("Login", "UserAuth");
        }

        // Get all accepted friendships involving the current user
        var friendEmails = _context.Friends
            .Where(f => f.IsAccepted && (f.RequesterEmail == myEmail || f.ReceiverEmail == myEmail))
            .Select(f => f.RequesterEmail == myEmail ? f.ReceiverEmail : f.RequesterEmail)
            .Distinct()
            .ToList();

        // Get the Account records for those emails
        var friends = _context.Accounts
            .Where(a => friendEmails.Contains(a.Email))
            .ToList();

        return View(friends);
    }

}
