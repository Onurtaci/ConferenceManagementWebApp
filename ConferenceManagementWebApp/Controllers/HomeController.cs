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
            return RedirectToAction("Index", "Admin");
        }
        else if (User.IsInRole("Organizer"))
        {
            return RedirectToAction("Index", "Organizer");
        }
        else if (User.IsInRole("Reviewer"))
        {
            return RedirectToAction("Index", "Reviewer");
        }
        else if (User.IsInRole("Presenter"))
        {
            return RedirectToAction("Index", "Presenter");
        }
        else if (User.IsInRole("Attendee"))
        {
            return RedirectToAction("Index", "Attendee");
        }
        else if (User.IsInRole("Author"))
        {
            return RedirectToAction("Index", "Author");
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
