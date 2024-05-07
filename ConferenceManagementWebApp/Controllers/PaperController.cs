using ConferenceManagementWebApp.Data;
using ConferenceManagementWebApp.Models;
using ConferenceManagementWebApp.ViewModels.PaperViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ConferenceManagementWebApp.Controllers;

[Authorize(Roles = "Author")]
public class PaperController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public PaperController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Create(string sessionId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        var session = _context.Sessions.Find(sessionId);
        if (session == null)
        {
            return NotFound();
        }

        var model = new PaperCreateViewModel
        {
            SessionId = sessionId
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PaperCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        var session = _context.Sessions.Find(model.SessionId);
        if (session == null)
        {
            return NotFound();
        }

        var paper = new Paper
        {
            Id = Guid.NewGuid().ToString(),
            Title = model.Title,
            Abstract = model.Abstract,
            Keywords = model.Keywords,
            Session = session,
            Author = user
        };

        if (model.File != null)
        {
            using var memoryStream = new MemoryStream();
            await model.File.CopyToAsync(memoryStream);
            paper.FileBytes = memoryStream.ToArray();
        }

        _context.Papers.Add(paper);
        await _context.SaveChangesAsync();

        var notification = new Notification
        {
            Id = Guid.NewGuid().ToString(),
            Message = $"Paper {paper.Title} has been submitted",
            CreationDate = DateTime.Now,
            User = user
        };

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();

        var review = new Review
        {
            Id = Guid.NewGuid().ToString(),
            PaperId = paper.Id,
            Reviewer = GetRandomReviewer(session.Id),
            Paper = paper,
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }

    private ApplicationUser GetRandomReviewer(string sessionId)
    {
        var conference = _context.Conferences
            .Include(c => c.Sessions)
            .FirstOrDefault(c => c.Sessions.Any(s => s.Id == sessionId));

        var conferenceReviewers = _context.ConferenceReviewers.Where(cr => cr.ConferenceId == conference.Id).ToList();

        var random = new Random();

        var randomReviewerId = conferenceReviewers[random.Next(conferenceReviewers.Count)].ReviewerId;

        return _context.Users.Find(randomReviewerId);
    }
}
