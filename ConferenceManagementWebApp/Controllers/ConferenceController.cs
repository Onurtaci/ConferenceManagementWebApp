﻿using ConferenceManagementWebApp.Data;
using ConferenceManagementWebApp.Enums;
using ConferenceManagementWebApp.Models;
using ConferenceManagementWebApp.ViewModels.ConferenceViewModels;
using ConferenceManagementWebApp.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                        PresentationType = (PresentationTypes)Enum.Parse(typeof(PresentationTypes), sessionData.PresentationType.ToString()),
                        Presenter = presenter,
                        Conference = conference
                    };

                    conference.Sessions.Add(session);

                    await _context.Sessions.AddAsync(session);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        var allReviewers = await _userManager.GetUsersInRoleAsync("Reviewer");
        var allPresenters = await _userManager.GetUsersInRoleAsync("Presenter");

        model.AllReviewers = allReviewers.ToList();
        model.AllPresenters = allPresenters.ToList();

        return View(model);
    }


    public async Task<IActionResult> List()
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
            .Include(c => c.ConferenceReviewers)
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


    [Authorize(Roles = "Attendee, Presenter, Reviewer, Author")]
    public async Task<IActionResult> Join(string conferenceId)
    {
        var conference = await _context.Conferences
            .FirstOrDefaultAsync(c => c.Id == conferenceId);

        var attendee = await _userManager.GetUserAsync(User);

        if (conference is not null && attendee is not null)
        {
            var conferenceAttendee = new ConferenceAttendee
            {
                Id = Guid.NewGuid().ToString(),
                Attendee = attendee,
                Conference = conference
            };

            foreach (var attendeeInConference in conference.ConferenceAttendees)
            {
                if (attendeeInConference.AttendeeId == attendee.Id)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            conference.ConferenceAttendees.Add(conferenceAttendee);

            await _context.ConferenceAttendees.AddAsync(conferenceAttendee);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index", "Home");
    }


    [Authorize(Roles = "Attendee, Presenter, Reviewer, Author")]
    public async Task<IActionResult> Disjoin(string conferenceId)
    {
        var conference = await _context.Conferences
            .FirstOrDefaultAsync(c => c.Id == conferenceId);

        var user = await _userManager.GetUserAsync(User);

        if (conference is not null && user is not null)
        {
            var conferenceAttendee = await _context.ConferenceAttendees
                .FirstOrDefaultAsync(ca => ca.ConferenceId == conferenceId && ca.AttendeeId == user.Id);

            if (conferenceAttendee is not null)
            {
                conference.ConferenceAttendees.Remove(conferenceAttendee);

                _context.ConferenceAttendees.Remove(conferenceAttendee);
                await _context.SaveChangesAsync();
            }
        }

        return RedirectToAction("Index", "Home");
    }


    //[Authorize(Roles = "Attendee, Presenter, Reviewer, Author")]
    //public async Task<IActionResult> GiveFeedback(string conferenceId)
    //{
    //    var conference = await _context.Conferences
    //        .Include(c => c.Sessions)
    //        .FirstOrDefaultAsync(c => c.Id == conferenceId);

    //    var user = await _userManager.GetUserAsync(User);

    //    var model = new ConferenceFeedbackViewModel
    //    {
    //        ConferenceId = conferenceId,
    //        UserId = user.Id
    //    };

    //    return View(model);
    //}

    //[HttpPost]
    //[Authorize(Roles = "Attendee, Presenter, Reviewer, Author")]
    //public async Task<IActionResult> GiveFeedback(ConferenceFeedbackViewModel model)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        var conference = await _context.Conferences
    //            .Include(c => c.Sessions)
    //            .FirstOrDefaultAsync(c => c.Id == model.ConferenceId);

    //        var user = await _userManager.GetUserAsync(User);

    //        var feedback = new Feedback
    //        {
    //            Id = Guid.NewGuid().ToString(),
    //            Rating = model.Rating,
    //            Comment = model.Comment,
    //            Attendee = user,
    //            Conference = conference
    //        };

    //        conference.Feedbacks.Add(feedback);

    //        await _context.Feedbacks.AddAsync(feedback);
    //        await _context.SaveChangesAsync();
    //    }

    //    return View(model);
    //}
}
