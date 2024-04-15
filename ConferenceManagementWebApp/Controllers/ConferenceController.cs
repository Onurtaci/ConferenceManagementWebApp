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
            var conference = new Conference
            {
                Title = model.Title,
                Description = model.Description,
                Venue = model.Venue,
                StartTime = model.StartTime,
                EndTime = model.EndTime
            };

            _context.Conferences.Add(conference);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(model);
    }

    public IActionResult Edit()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Edit(ConferenceEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var conference = _context.Conferences.Find(model.Id);

            if(conference == null)
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

        if(conference == null)
        {
            return NotFound();
        }

        _context.Conferences.Remove(conference);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}
