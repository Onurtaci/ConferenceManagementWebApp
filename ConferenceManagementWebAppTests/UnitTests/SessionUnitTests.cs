using ConferenceManagementWebApp.Controllers;
using ConferenceManagementWebApp.Data;
using ConferenceManagementWebApp.Enums;
using ConferenceManagementWebApp.Models;
using ConferenceManagementWebApp.ViewModels.SessionViewModels;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceManagementWebAppTests.UnitTests;

public class SessionUnitTests : IDisposable
{
    private ApplicationDbContext _context;
    private UserManager<ApplicationUser> _userManager;
    private SessionController _sessionController;

    [TearDown]
    public void Dispose()
    {
        _sessionController = null;
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
        _sessionController = new SessionController(_context, _userManager);
    }

    [Test]
    public async Task List_ReturnsNotFound_WhenConferenceIsNull()
    {
        // Arrange
        var conferenceId = Guid.NewGuid().ToString();

        // Act
        var result = await _sessionController.List(conferenceId);

        // Assert
        Assert.IsTrue(result is NotFoundResult);
    }

    [Test]
    public async Task List_ReturnsViewResult_WhenConferenceIsNotNull()
    {
        // Arrange
        var conference = new Conference
        {
            Id = Guid.NewGuid().ToString(),
            Title = "Conference 1",
            Description = "Description 1",
            Venue = "Venue 1",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(7),
            Organizer = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane",
                UserName = "jane",
            },
            Sessions = new List<Session>
            {
                new Session
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Session 1",
                    Topic = "Topic 1",
                    StartTime = DateTime.Now.AddDays(1),
                    EndTime = DateTime.Now.AddDays(4),
                    PresentationType = PresentationTypes.Keynote,
                    Presenter = new ApplicationUser
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john@email.com",
                        UserName = "john"
                    }
                }
            }
        };

        _context.Conferences.Add(conference);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sessionController.List(conference.Id);

        // Assert
        Assert.IsTrue((result as ViewResult)?.Model is List<SessionListViewModel>);
    }
}
