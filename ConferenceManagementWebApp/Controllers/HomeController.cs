using ConferenceManagementWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ConferenceManagementWebApp.Controllers;

[Authorize]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (User.IsInRole("Admin"))
        {
            return View("AdminIndex");
        }
        else if (User.IsInRole("Organizer"))
        {
            return View("OrganizerIndex");
        }
        else if (User.IsInRole("Attendee"))
        {
            return View("AttendeeIndex");
        }
        else if (User.IsInRole("Reviewer"))
        {
            return View("ReviewerIndex");
        }
        else if (User.IsInRole("Author"))
        {
            return View("AuthorIndex");
        }
        else
        {
            return View();
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
