using ConferenceManagementWebApp.Constants;
using ConferenceManagementWebApp.Data;
using ConferenceManagementWebApp.Enums;
using ConferenceManagementWebApp.Models;
using ConferenceManagementWebApp.ViewModels.PaperViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManagementWebApp.Controllers;

[Authorize]
public class PaperController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public PaperController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    [Authorize(Roles = "Author")]
    public async Task<IActionResult> Create(string sessionId)
    {
        var user = await _userManager.GetUserAsync(User);

        var conference = _context.Conferences
            .Include(c => c.Sessions)
            .FirstOrDefault(c => c.Sessions.Any(s => s.Id == sessionId));

        if (await _context.ConferenceAttendees.AnyAsync(ca => ca.ConferenceId == conference.Id && ca.AttendeeId == user.Id) == false)
        {
            return RedirectToAction("List", "Session", routeValues: new { conferenceId = conference.Id });
        }

        var session = _context.Sessions.Find(sessionId);
        if (session == null)
        {
            return NotFound();
        }

        var model = new PaperCreateViewModel
        {
            SessionId = sessionId
        };

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "Author")]
    public async Task<IActionResult> Create(PaperCreateViewModel model)
    {
        if (!ModelState.IsValid || !IsPdf(model.File))
        {
            ModelState.AddModelError("File", Messages.FileTypeInvalid);
            return View(model);
        }

        var user = await _userManager.GetUserAsync(User);

        var session = _context.Sessions.Find(model.SessionId);
        if (session == null)
        { return NotFound(); }

        var paper = new Paper
        {
            Id = Guid.NewGuid().ToString(),
            Title = model.Title,
            Abstract = model.Abstract,
            Keywords = model.Keywords,
            Recommendation = Recommendation.None,
            Session = session,
            Author = user
        };

        if (model.File != null)
        {
            using var memoryStream = new MemoryStream();
            await model.File.CopyToAsync(memoryStream);
            paper.FileBytes = memoryStream.ToArray();
        }
        else
        { return NotFound(); }

        _context.Papers.Add(paper);
        await _context.SaveChangesAsync();

        var review = new Review
        {
            Id = Guid.NewGuid().ToString(),
            PaperId = paper.Id,
            Reviewer = GetRandomReviewer(session.Id),
            Paper = paper,
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Authorize(Roles = "Author")]
    public async Task<IActionResult> ListPaperReviews()
    {
        var user = await _userManager.GetUserAsync(User);

        var papers = _context.Papers
            .Include(p => p.Session)
            .Include(p => p.Review)
            .Where(p => p.Author.Id == user.Id)
            .ToList();

        var model = new List<PaperListReviewedViewModel>();

        foreach (var paper in papers)
        {
            if (paper.Recommendation == Recommendation.None)
            {
                continue;
            }

            model.Add(new PaperListReviewedViewModel
            {
                Title = paper.Title,
                Score = paper.Review.Score,
                Comment = paper.Review.Comment,
                Recommendation = paper.Review.Recommendation
            });
        }

        return View(model);
    }

    [HttpGet]
    [Authorize(Roles = "Reviewer")]
    public async Task<IActionResult> ListAssignedPapers()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        var papers = new List<PaperListAssignedViewModel>();

        var reviews = _context.Reviews
            .Include(r => r.Paper)
            .ThenInclude(p => p.Session)
            .Include(r => r.Reviewer)
            .Where(r => r.Reviewer.Id == user.Id)
            .ToList();

        foreach (var review in reviews)
        {
            papers.Add(new PaperListAssignedViewModel
            {
                Id = review.Paper.Id,
                SessionId = review.Paper.Session.Id,
                Title = review.Paper.Title,
                Abstract = review.Paper.Abstract,
                Keywords = review.Paper.Keywords,
                Recommendation = review.Recommendation
            });
        }

        return View(papers);
    }

    [HttpGet]
    [Authorize(Roles = "Reviewer")]
    public async Task<IActionResult> Review(string paperId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        var paper = _context.Papers.Find(paperId);
        if (paper == null)
        {
            return NotFound();
        }

        var review = _context.Reviews
            .Include(r => r.Paper)
            .Include(r => r.Reviewer)
            .FirstOrDefault(r => r.Paper.Id == paperId && r.Reviewer.Id == user.Id);

        if (review == null)
        {
            return NotFound();
        }

        var model = new PaperReviewViewModel
        {
            PaperId = paperId
        };

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "Reviewer")]
    public async Task<IActionResult> Review(PaperReviewViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.GetUserAsync(User);

        var paper = _context.Papers.Include(p => p.Author).FirstOrDefault(p => p.Id == model.PaperId);

        paper.Recommendation = model.Recommendation;

        _context.Papers.Update(paper);
        await _context.SaveChangesAsync();

        var review = _context.Reviews
            .Include(r => r.Paper)
            .Include(r => r.Reviewer)
            .FirstOrDefault(r => r.Paper.Id == model.PaperId && r.Reviewer.Id == user.Id);

        if (review == null)
        {
            return NotFound();
        }

        review.Score = int.Parse(model.Score);
        review.Comment = model.Comment;
        review.Recommendation = model.Recommendation;

        _context.Reviews.Update(review);
        await _context.SaveChangesAsync();

        paper.Recommendation = model.Recommendation;

        _context.Papers.Update(paper);
        await _context.SaveChangesAsync();

        var notification = new Notification
        {
            Id = Guid.NewGuid().ToString(),
            Message = $"Paper {paper.Title} has been reviewed",
            CreationDate = DateTime.Now,
            Receiver = paper.Author
        };

        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();



        return RedirectToAction("ListAssignedPapers");
    }

    private ApplicationUser GetRandomReviewer(string sessionId)
    {
        var conference = _context.Conferences
            .Include(c => c.Sessions)
            .FirstOrDefault(c => c.Sessions.Any(s => s.Id == sessionId));

        var conferenceReviewers = _context.ConferenceReviewers.Where(cr => cr.ConferenceId == conference.Id).ToList();

        var random = new Random();

        var randomReviewerId = conferenceReviewers[random.Next(conferenceReviewers.Count)].ReviewerId;

        return _context.Users.Find(randomReviewerId);
    }

    [HttpGet]
    [Authorize(Roles = "Author, Reviewer")]
    public async Task<IActionResult> Download(string paperId)
    {
        var paper = _context.Papers.Find(paperId);
        if (paper == null)
        {
            return NotFound();
        }

        return File(paper.FileBytes, "application/pdf", $"{paper.Title}.pdf");
    }

    public bool IsPdf(IFormFile file)
    {
        return file.ContentType == "application/pdf";
    }
}
