using Microsoft.AspNetCore.Mvc;
using TrueMatch.Models.Data;
using TrueMatch.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Linq;
using TrueMatch.Models;

public class MessageController : Controller
{
    private readonly ApplicationDbContext _context;

    public MessageController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult SendMessage(MessageViewModel model)
    {
        var senderEmail = HttpContext.Session.GetString("Email");

        if (string.IsNullOrEmpty(senderEmail))
        {
            return RedirectToAction("Login", "UserAuth");
        }
        if (string.IsNullOrEmpty(model.ReceiverEmail))
        {
            return Content("ReceiverEmail is null!");
        }

        var message = new Message
        {
            MsgId = Guid.NewGuid(),
            SenderEmail = senderEmail,
            ReceiverEmail = model.ReceiverEmail,
            MessageText = model.MessageText,
            MsgTime = DateTime.Now
        };

        _context.Messages.Add(message);
        _context.SaveChanges();

        return RedirectToAction("Chat", new { withUser = model.ReceiverEmail });
    }

    public IActionResult Chat(string withUser)
    {
        var senderEmail = HttpContext.Session.GetString("Email");

        if (string.IsNullOrEmpty(senderEmail))
        {
            return RedirectToAction("Login", "UserAuth");
        }

        var messages = _context.Messages
            .Where(m =>
                (m.SenderEmail == senderEmail && m.ReceiverEmail == withUser) ||
                (m.SenderEmail == withUser && m.ReceiverEmail == senderEmail))
            .OrderBy(m => m.MsgTime)
            .ToList();
        ViewBag.ReceiverEmail = withUser;
        return View(messages);
    }
    [HttpGet]
    public IActionResult AllMessage(Message msg)
    {
        var senderEmail = HttpContext.Session.GetString("Email");

        if (string.IsNullOrEmpty(senderEmail))
        {
            return RedirectToAction("Login", "UserAuth");
        }
        var allMessage = _context.Messages.Where(m =>m.SenderEmail==senderEmail && m.ReceiverEmail != null).Select(m=>m.ReceiverEmail).Distinct().ToList();
        return View(allMessage);
    }
}
