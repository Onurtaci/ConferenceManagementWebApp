using ConferenceManagementWebApp.Data;
using ConferenceManagementWebApp.Models;
using ConferenceManagementWebApp.ViewModels.FeedbackViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManagementWebApp.Controllers;

public class FeedbackController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public FeedbackController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Create(string conferenceId)
    {
        var user = await _userManager.GetUserAsync(User);

        var model = new FeedbackCreateViewModel
        {
            ConferenceId = conferenceId,
            UserId = user.Id
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(FeedbackCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var feedback = new Feedback
        {
            Id = Guid.NewGuid().ToString(),
            Conference = _context.Conferences.Find(model.ConferenceId),
            Attendee = await _userManager.FindByIdAsync(model.UserId),
            Rating = model.Rating,
            Comment = model.Comment
        };

        _context.Feedbacks.Add(feedback);
        await _context.SaveChangesAsync();

        return RedirectToAction("ListAttendedConferences", "Conference");
    }
}
