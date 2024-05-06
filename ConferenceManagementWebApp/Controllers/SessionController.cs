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

    //public async Task <IActionResult> List(string conferenceId)
    //{
    //    var conference = await _context.Conferences
    //        .Include(c => c.Sessions)
    //        .FirstOrDefaultAsync(c => c.Id == conferenceId);

    //    var model = new SessionListViewModel
    //    {
    //        ConferenceId = conferenceId,
    //        Sessions = conference.Sessions
    //    };

    //    return View(model);
    //}
}