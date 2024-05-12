using ConferenceManagementWebApp.Controllers;
using ConferenceManagementWebApp.Data;
using ConferenceManagementWebApp.Enums;
using ConferenceManagementWebApp.Models;
using ConferenceManagementWebApp.ViewModels.PaperViewModels;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceManagementWebAppTests.UnitTests;

public class PaperUnitTests : IDisposable
{
    private ApplicationDbContext _context;
    private UserManager<ApplicationUser> _userManager;
    private PaperController _paperController;

    [TearDown]
    public void Dispose()
    {
        _paperController = null;
        _userManager = null;
        _context = null;
    }

    [SetUp]
    public void Setup()
    {
        _context = new ApplicationDbContext(options:
            new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options);
        _userManager = A.Fake<UserManager<ApplicationUser>>();
        _paperController = new PaperController(_context, _userManager);
    }

    [Test]
    public async Task Download_ReturnsNotFound_WhenPaperIsNull()
    {
        // Arrange
        var paperId = Guid.NewGuid().ToString();

        // Act
        var result = await _paperController.Download(paperId);

        // Assert
        Assert.IsTrue(result is NotFoundResult);
    }

    [Test]
    public async Task Create_Paper_WhenFileContentTypeInvalid()
    {
        // Arrange
        var sessionId = Guid.NewGuid().ToString();
        var conference = new Conference
        {
            Id = Guid.NewGuid().ToString(),
            Title = "Test Conference",
            Description = "Test Conference Description",
            Venue = "Test Venue",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(7),
            Organizer = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Test",
                LastName = "Organizer",
                Email = "test",
                UserName = "test"
            },
            Sessions = new List<Session>
            {
                new Session
                {
                    Id = sessionId,
                    Title = "Test Session",
                    Topic = "Test Topic",
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(1),
                    PresentationType = PresentationTypes.Workshop,
                    Presenter = new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        FirstName = "Test",
                        LastName = "Presenter",
                        Email = "test",
                        UserName = "test"
                    }
                }
            }
        };

        _context.Conferences.Add(conference);
        await _context.SaveChangesAsync();

        var txtFilePath = "D:\\Projects\\VisualStudioProjects\\ConferenceManagementWebApp\\ConferenceManagementWebAppTests\\test.txt";
        using (var fileStream = new FileStream(txtFilePath, FileMode.Open))
        {
            var file = new FormFile(fileStream, 0, fileStream.Length, "file", "file.txt")
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/plain"
            };

            var model = new PaperCreateViewModel
            {
                SessionId = sessionId,
                Title = "Test Paper",
                Abstract = "Test Abstract",
                Keywords = "Test Keywords",
                File = file
            };

            // Act
            var result = await _paperController.Create(model);

            // Assert
            Assert.IsTrue((result as ViewResult).ViewData.ModelState.ErrorCount == 1);
        }
    }
}
