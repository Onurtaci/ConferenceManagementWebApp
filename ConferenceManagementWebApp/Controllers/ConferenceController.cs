using ConferenceManagementWebApp.Data;
using ConferenceManagementWebApp.Enums;
using ConferenceManagementWebApp.Models;
using ConferenceManagementWebApp.ViewModels.ConferenceViewModels;
using ConferenceManagementWebApp.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConferenceManagementWebApp.Controllers;

[Authorize(Roles = "Organizer")]
public class ConferenceController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ConferenceController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Create()
    {
        var reviewers = await _userManager.GetUsersInRoleAsync("Reviewer");
        var presenters = await _userManager.GetUsersInRoleAsync("Presenter");

        var model = new ConferenceCreateViewModel
        {
            AllReviewers = reviewers.ToList(),
            AllPresenters = presenters.ToList()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ConferenceCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            var conference = new Conference
            {
                Id = Guid.NewGuid().ToString(),
                Title = model.Title,
                Description = model.Description,
                Venue = model.Venue,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Organizer = await _userManager.GetUserAsync(User)
            };

            await _context.Conferences.AddAsync(conference);
            await _context.SaveChangesAsync();

            var selectedReviewers = JsonConvert.DeserializeObject<List<string>>(model.SelectedReviewers[0]);

            foreach (var reviewerId in selectedReviewers)
            {
                var reviewer = await _userManager.FindByIdAsync(reviewerId);

                if (reviewer is not null)
                {
                    var conferenceReviewer = new ConferenceReviewer
                    {
                        Id = Guid.NewGuid().ToString(),
                        Reviewer = reviewer,
                        Conference = conference
                    };

                    conference.ConferenceReviewers.Add(conferenceReviewer);

                    await _context.ConferenceReviewers.AddAsync(conferenceReviewer);
                    await _context.SaveChangesAsync();
                }
            }

            var sessionsData = JsonConvert.DeserializeObject<List<SessionDataViewModel>>(model.SessionsData[0]);

            foreach (var sessionData in sessionsData)
            {
                var presenter = await _userManager.FindByIdAsync(sessionData.PresenterId);

                if (presenter is not null)
                {
                    var session = new Session
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = sessionData.Title,
                        Topic = sessionData.Topic,
                        StartTime = sessionData.StartTime,
                        EndTime = sessionData.EndTime,
                        PresentationType = (PresentationTypes) Enum.Parse(typeof(PresentationTypes), sessionData.PresentationType.ToString()),
                        Presenter = presenter,
                        Conference = conference
                    };

                    conference.Sessions.Add(session);

                    await _context.Sessions.AddAsync(session);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index","Home");
        }

        return View(model);
    }
}
