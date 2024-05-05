using ConferenceManagementWebApp.Data;
using ConferenceManagementWebApp.Enums;
using ConferenceManagementWebApp.Models;
using ConferenceManagementWebApp.ViewModels.ConferenceViewModels;
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

            var selectedReviewers = JsonConvert.DeserializeObject<List<string>>(model.SelectedReviewers[0]);

            foreach (var reviewerId in selectedReviewers)
            {
                var reviewer = await _userManager.FindByIdAsync(reviewerId);

                if (reviewer is not null)
                {
                    conference.ConferenceReviewers.Add(new ConferenceReviewer
                    {
                        Conference = conference,
                        Reviewer = reviewer
                    });
                }
            }

            var sessionsData = model.SessionsData;

            //foreach (var sessionData in sessionsData)
            //{
            //    var session = new Session
            //    {
            //        Id = Guid.NewGuid().ToString(),
            //        Title = sessionData.Title,
            //        Topic = sessionData.Topic,
            //        StartTime = sessionData.StartTime,
            //        EndTime = sessionData.EndTime,
            //        Presenter = await _userManager.FindByIdAsync(sessionData.PresenterId),
            //        Conference = conference
            //    };

            //    conference.Sessions.Add(session);
            //}

            await _context.Conferences.AddAsync(conference);
            await _context.SaveChangesAsync();

            return View("Home/Index");
        }

        return View(model);
    }

    public IActionResult Edit(string Id)
    {
        var conference = _context.Conferences.Find(Id);

        if (conference == null)
        {
            return NotFound();
        }

        var model = new ConferenceEditViewModel
        {
            Id = conference.Id,
            Title = conference.Title,
            Description = conference.Description,
            Venue = conference.Venue,
            StartTime = conference.StartTime,
            EndTime = conference.EndTime,
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(ConferenceEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var conference = _context.Conferences.Find(model.Id);

            if (conference == null)
            {
                return NotFound();
            }

            conference.Title = model.Title;
            conference.Description = model.Description;
            conference.Venue = model.Venue;
            conference.StartTime = model.StartTime;
            conference.EndTime = model.EndTime;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(model);
    }

    public IActionResult Delete(string Id)
    {
        var conference = _context.Conferences.Find(Id);

        if (conference == null)
        {
            return NotFound();
        }

        _context.Conferences.Remove(conference);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult List()
    {
        var conferences = _context.Conferences.ToList();

        return View(conferences);
    }
}
