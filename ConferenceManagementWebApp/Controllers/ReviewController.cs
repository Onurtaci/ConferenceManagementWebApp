using ConferenceManagementWebApp.Data;
using ConferenceManagementWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManagementWebApp.Controllers;

//[Authorize(Roles = "Reviewer")]
public class ReviewController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ReviewController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult List()
    {
        var user = _userManager.GetUserAsync(User).Result;
        if (user == null)
        {
            return NotFound();
        }
        var reviews = _context.Reviews.Where(r => r.Reviewer.Id == user.Id).ToList();
        return View(reviews);
    }

    public async Task<IActionResult> ReviewPaper(string reviewId)
    {
        return View();
    }
}
