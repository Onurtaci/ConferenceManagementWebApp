using ConferenceManagementWebApp.Controllers;
using ConferenceManagementWebApp.Data;
using ConferenceManagementWebApp.Enums;
using ConferenceManagementWebApp.Models;
using ConferenceManagementWebApp.ViewModels.ConferenceViewModels;
using ConferenceManagementWebApp.ViewModels.SessionViewModels;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ConferenceManagementWebAppTests.UnitTests;

public class ConferenceUnitTests : IDisposable
{
    private ApplicationDbContext _context;
    private UserManager<ApplicationUser> _userManager;
    private ConferenceController _conferenceController;

    [TearDown]
    public void Dispose()
    {
        _conferenceController = null;
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
        _conferenceController = new ConferenceController(_context, _userManager);
    }

    [Test]
    public async Task Join_Conference_ReturnsBadRequest_WhenConferenceIdIsNull()
    {
        // Arrange
        var conferenceId = (string?)null;

        // Act
        var result = await _conferenceController.Join(conferenceId);

        // Assert
        Assert.IsTrue((result as RedirectToActionResult)?.ActionName == "Index");
        Assert.IsTrue((result as RedirectToActionResult)?.ControllerName == "Home");
    }

    [Test]
    public async Task Create_Conference_ReturnsViewResult_WhenUserIsAuthenticated()
    {
        // Arrange
        var user = new ApplicationUser { Id = Guid.NewGuid().ToString() };
        A.CallTo(() => _userManager.GetUserAsync(A<ClaimsPrincipal>._)).Returns(user);

        // Act
        var result = await _conferenceController.Create();

        // Assert
        Assert.IsTrue(result is ViewResult);
    }
}