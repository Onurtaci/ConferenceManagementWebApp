using ConferenceManagementWebApp.Data;
using ConferenceManagementWebApp.Models;
using ConferenceManagementWebApp.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManagementWebApp.Controllers;

[Authorize]
public class SessionController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public SessionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> List(string conferenceId)
    {
        var model = new List<SessionListViewModel>();

        var conference = await _context.Conferences
            .Include(c => c.Sessions)
            .Include(c => c.Organizer)
            .FirstOrDefaultAsync(c => c.Id == conferenceId);

        if (conference == null)
        {
            return NotFound();
        }

        foreach (var conferenceSession in conference.Sessions)
        {
            var session = await _context.Sessions
                .Include(s => s.Presenter)
                .FirstOrDefaultAsync(s => s.Id == conferenceSession.Id);

            if (session != null)
            {
                model.Add(new SessionListViewModel
                {
                    SessionId = session.Id,
                    Title = session.Title,
                    Topic = session.Topic,
                    StartTime = session.StartTime,
                    EndTime = session.EndTime,
                    PresentationType = session.PresentationType,
                    PresenterFullName = session.Presenter.FirstName + " " + session.Presenter.LastName
                });
            }
        }

        return View(model);
    }
}