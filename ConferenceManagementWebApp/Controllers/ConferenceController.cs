using ConferenceManagementWebApp.Constants;
using ConferenceManagementWebApp.Data;
using ConferenceManagementWebApp.Enums;
using ConferenceManagementWebApp.Models;
using ConferenceManagementWebApp.ViewModels.ConferenceViewModels;
using ConferenceManagementWebApp.ViewModels.SessionViewModels;
using ConferenceManagementWebApp.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ConferenceManagementWebApp.Controllers;

[Authorize]
public class ConferenceController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ConferenceController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [Authorize(Roles = "Organizer")]
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
    [Authorize(Roles = "Organizer")]
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
                StartDate = model.StartDate,
                EndDate = model.EndDate,
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

                    await _context.ConferenceReviewers.AddAsync(conferenceReviewer);
                    await _context.SaveChangesAsync();
                }
            }

            var sessionsData = JsonConvert.DeserializeObject<List<SessionDataViewModel>>(model.SessionsData[0]);

            if (sessionsData is not null)
            {
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
                            PresentationType = (PresentationTypes)Enum.Parse(typeof(PresentationTypes), sessionData.PresentationType.ToString()),
                            Presenter = presenter,
                            Conference = conference
                        };

                        if (session.StartTime < conference.StartDate || session.EndTime > conference.EndDate)
                        {
                            ModelState.AddModelError(string.Empty, Messages.SessionNotCreated);
                            return View(model);
                        }

                        await _context.Sessions.AddAsync(session);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, Messages.ConferenceNotCreated);
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        var allReviewers = await _userManager.GetUsersInRoleAsync("Reviewer");
        var allPresenters = await _userManager.GetUsersInRoleAsync("Presenter");

        model.AllReviewers = allReviewers.ToList();
        model.AllPresenters = allPresenters.ToList();

        return View(model);
    }

    public async Task<IActionResult> ListUpcomingConferences()
    {
        var conferences = await _context.Conferences
            .Include(c => c.Organizer)
            .Include(c => c.ConferenceReviewers)
            .Include(c => c.Sessions)
            .ToListAsync();

        var model = new List<ConferenceListViewModel>();

        foreach (var conference in conferences)
        {
            var conferenceModel = new ConferenceListViewModel
            {
                Id = conference.Id,
                Title = conference.Title,
                Description = conference.Description,
                Venue = conference.Venue,
                StartDate = conference.StartDate,
                EndDate = conference.EndDate,
                OrganizerFullName = $"{conference.Organizer.FirstName} {conference.Organizer.LastName}",
                Sessions = conference.Sessions
            };

            model.Add(conferenceModel);
        }

        return View(model);
    }

    [Authorize(Roles = "Attendee, Presenter, Reviewer, Author")]
    public async Task<IActionResult> ListAttendedConferences()
    {
        var user = await _userManager.GetUserAsync(User);

        var conferences = await _context.Conferences
            .Include(c => c.Organizer)
            .Include(c => c.Sessions)
            .Include(c => c.ConferenceAttendees)
            .ToListAsync();

        var model = new List<ConferenceListViewModel>();

        foreach (var conference in conferences)
        {
            foreach (var attendee in conference.ConferenceAttendees)
            {
                if (attendee.AttendeeId == user.Id)
                {
                    var conferenceModel = new ConferenceListViewModel
                    {
                        Id = conference.Id,
                        Title = conference.Title,
                        Description = conference.Description,
                        Venue = conference.Venue,
                        StartDate = conference.StartDate,
                        EndDate = conference.EndDate,
                        OrganizerFullName = $"{conference.Organizer.FirstName} {conference.Organizer.LastName}",
                        Sessions = conference.Sessions
                    };

                    model.Add(conferenceModel);
                }
            }
        }

        return View(model);
    }

    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> ListOrganizedConferences()
    {
        var user = await _userManager.GetUserAsync(User);

        var conferences = await _context.Conferences
            .Include(c => c.Organizer)
            .Include(c => c.Sessions)
            .Where(c => c.Organizer.Id == user.Id)
            .ToListAsync();

        var model = new List<ConferenceListOrganizedConferencesViewModel>();

        foreach (var conference in conferences)
        {
            var conferenceModel = new ConferenceListOrganizedConferencesViewModel
            {
                Id = conference.Id,
                Title = conference.Title,
                Description = conference.Description,
                Venue = conference.Venue,
                StartDate = conference.StartDate,
                EndDate = conference.EndDate,
                Sessions = conference.Sessions,
                Attendees = conference.ConferenceAttendees.Select(ca => ca.Attendee).ToList()
            };

            model.Add(conferenceModel);
        }

        return View(model);
    }

    [Authorize(Roles = "Attendee, Presenter, Reviewer, Author")]
    public async Task<IActionResult> Join(string conferenceId)
    {
        var conference = await _context.Conferences.Include(c => c.ConferenceAttendees).FirstOrDefaultAsync(c => c.Id == conferenceId);
        var attendee = await _userManager.GetUserAsync(User);

        if (conference is not null && attendee is not null)
        {
            if (conference.EndDate < DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, Messages.ConferenceEnded);
                return RedirectToAction("Index", "Home");
            }

            var conferenceAttendee = new ConferenceAttendee
            {
                Id = Guid.NewGuid().ToString(),
                Attendee = attendee,
                Conference = conference
            };

            if (conference.ConferenceAttendees.Any(ca => ca.AttendeeId == attendee.Id))
            {
                ModelState.AddModelError(string.Empty, Messages.AlreadyJoined);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                await _context.ConferenceAttendees.AddAsync(conferenceAttendee);
                await _context.SaveChangesAsync();
            }
        }

        return RedirectToAction("Index", "Home");
    }

    [Authorize(Roles = "Attendee, Presenter, Reviewer, Author")]
    public async Task<IActionResult> Disjoin(string conferenceId)
    {
        var conference = await _context.Conferences.FirstOrDefaultAsync(c => c.Id == conferenceId);
        var user = await _userManager.GetUserAsync(User);

        if (conference is not null && user is not null)
        {
            var conferenceAttendee = await _context.ConferenceAttendees.FirstOrDefaultAsync(ca => ca.ConferenceId == conferenceId && ca.AttendeeId == user.Id);

            if (conferenceAttendee is not null)
            {
                _context.ConferenceAttendees.Remove(conferenceAttendee);
                await _context.SaveChangesAsync();
            }
        }

        return RedirectToAction("Index", "Home");
    }

    [Authorize(Roles = "Organizer")]
    public async Task<IActionResult> ListAttendees(string conferenceId)
    {
        var conference = await _context.Conferences
            .Include(c => c.ConferenceAttendees)
            .FirstOrDefaultAsync(c => c.Id == conferenceId);

        var model = new List<UserListViewModel>();

        foreach (var attendee in conference.ConferenceAttendees)
        {
            var user = await _userManager.FindByIdAsync(attendee.AttendeeId);

            if (user is not null)
            {
                var userModel = new UserListViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email
                };

                model.Add(userModel);
            }
        }

        return View(model);
    }
}
