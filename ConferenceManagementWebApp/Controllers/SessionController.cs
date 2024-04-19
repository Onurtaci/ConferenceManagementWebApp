using ConferenceManagementWebApp.Data;
using ConferenceManagementWebApp.Models;
using ConferenceManagementWebApp.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManagementWebApp.Controllers;

//[Authorize(Roles = "Organizer")]
public class SessionController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public SessionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(SessionCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _context.Users.Find(model.PresenterId);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            if (!userRoles.Contains("Presenter"))
            {
                return BadRequest();
            }

            var conference = _context.Conferences.Find(model.ConferenceId);
            if (conference == null)
            {
                return NotFound();
            }

            var session = new Session
            {
                Id = Guid.NewGuid().ToString(),
                Title = model.Title,
                Topic = model.Topic,
                PresentationType = model.PresentationType,
                Presenter = user,
                Conference = conference,
            };

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            conference.Sessions.Add(session);

            return RedirectToAction("Index");
        }
        return View(model);
    }

    public IActionResult Edit(string id)
    {
        var session = _context.Sessions.Find(id);
        if (session == null)
        {
            return NotFound();
        }

        var model = new SessionEditViewModel
        {
            Id = session.Id,
            Title = session.Title,
            Topic = session.Topic,
            PresentationType = session.PresentationType,
            PresenterId = session.Presenter.Id,
            ConferenceId = session.Conference.Id,
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(SessionEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var session = _context.Sessions.Find(model.Id);

            if (session == null)
            {
                return NotFound();
            }

            session.Title = model.Title;
            session.Topic = model.Topic;
            session.PresentationType = model.PresentationType;

            _context.Sessions.Update(session);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var session = _context.Sessions.Find(id);
        if (session == null)
        {
            return NotFound();
        }

        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}