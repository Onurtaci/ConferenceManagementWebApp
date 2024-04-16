using ConferenceManagementWebApp.Data;
using ConferenceManagementWebApp.Models;
using ConferenceManagementWebApp.ViewModels.ConferenceViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManagementWebApp.Controllers;

//[Authorize(Roles = "Organizer")]
public class ConferenceController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ConferenceController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
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
    public IActionResult Create(ConferenceCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            var organizer = _userManager.GetUserAsync(User).Result;
            if (organizer == null)
            {
                return NotFound();
            }

            var conference = new Conference
            {
                Id = Guid.NewGuid().ToString(),
                Title = model.Title,
                Description = model.Description,
                Venue = model.Venue,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Organizer = organizer,
            };

            _context.Conferences.Add(conference);
            _context.SaveChanges();

            return RedirectToAction("Index");
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

    public IActionResult Delete()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Delete(string id)
    {
        var conference = _context.Conferences.Find(id);

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
