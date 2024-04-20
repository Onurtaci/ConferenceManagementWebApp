using ConferenceManagementWebApp.Data;
using ConferenceManagementWebApp.Models;
using ConferenceManagementWebApp.ViewModels.PaperViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManagementWebApp.Controllers;

[Authorize(Roles = "Author")]
public class PaperController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public PaperController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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
    public async Task<IActionResult> Create(PaperCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user == null)
            {
                return NotFound();
            }

            var session = _context.Sessions.Find(model.SessionId);
            if (session == null)
            {
                return NotFound();
            }

            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await model.File.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            var paper = new Paper
            {
                Id = Guid.NewGuid().ToString(),
                Title = model.Title,
                Abstract = model.Abstract,
                Keywords = model.Keywords,
                FileBytes = fileBytes,
                Author = user,
                Session = session,
                Status = Enums.Status.Submitted,
            };

            _context.Papers.Add(paper);
            await _context.SaveChangesAsync();

            session.Papers.Add(paper);

            if (model.RandomReviewer)
            {
                var reviewer = GetRandomRewiewer();
                var review = new Review
                {
                    Id = Guid.NewGuid().ToString(),
                    Paper = paper,
                    Reviewer = reviewer,
                };

                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();
            }
            else
            {
                var reviewer = _userManager.FindByIdAsync(model.ReviewerId).Result;
                if (reviewer == null)
                {
                    return NotFound();
                }

                var review = new Review
                {
                    Id = Guid.NewGuid().ToString(),
                    PaperId = paper.Id,
                    Reviewer = reviewer,
                    Paper = paper,
                };

                await _context.Reviews.AddAsync(review);
                await _context.SaveChangesAsync();

                var notification = new Notification
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = reviewer.Id,
                    Message = $"You have been assigned to review the paper {paper.Title}.",
                    CreationDate = DateTime.Now,
                };

                await _context.Notifications.AddAsync(notification);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        return View(model);
    }

    private ApplicationUser GetRandomRewiewer()
    {
        var reviewers = _userManager.GetUsersInRoleAsync("Reviewer").Result;
        var random = new Random();
        var index = random.Next(reviewers.Count);
        return reviewers[index];
    }
}
