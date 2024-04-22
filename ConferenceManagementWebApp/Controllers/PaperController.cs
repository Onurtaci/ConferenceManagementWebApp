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

            if (model.SelectedReviewerId != null)
            {

                var reviewer = _userManager.FindByIdAsync(model.SelectedReviewerId).Result;
                if (reviewer != null)
                {
                    AddReview(paper, reviewer);
                }
            }
            else
            {
                var randomReviewers = GetRandomReviewers(3);
                foreach (var reviewer in randomReviewers)
                {
                    AddReview(paper, reviewer);
                }
            }
            return RedirectToAction("Index");
        }
        return View(model);
    }

    private List<ApplicationUser> GetRandomReviewers(int numberOfReviewer)
    {
        var reviewers = _userManager.GetUsersInRoleAsync("Reviewer").Result;
        var random = new Random();
        var randomReviewers = new List<ApplicationUser>();
        for (int i = 0; i < numberOfReviewer; i++)
        {
            var index = random.Next(reviewers.Count);
            randomReviewers.Add(reviewers[index]);
            reviewers.RemoveAt(index);
        }
        return randomReviewers;
    }

    private bool AddReview(Paper paper, ApplicationUser reviewer)
    {
        var review = new Review
        {
            Id = Guid.NewGuid().ToString(),
            Reviewer = reviewer,
            Paper = paper
        };

        _context.Reviews.Add(review);
        var result = _context.SaveChanges();
        if (result > 0)
        {
            paper.Reviews.Add(review);

            var notification = new Notification
            {
                Id = Guid.NewGuid().ToString(),
                Message = $"You have been assigned to review the paper {paper.Title}.",
                CreationDate = DateTime.Now,
                User = reviewer
            };

            return true;
        }
        return false;
    }
}
