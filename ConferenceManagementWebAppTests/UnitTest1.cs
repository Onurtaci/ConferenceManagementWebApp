using ConferenceManagementWebApp.Controllers;
using ConferenceManagementWebApp.Data;
using ConferenceManagementWebApp.Models;
using ConferenceManagementWebApp.ViewModels.ConferenceViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace ConferenceManagementWebAppTests;

[TestFixture]
public class ConferenceControllerTest
{
    [Test]
    public void CreateConferenceTest()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "CreateConferenceTest")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context),
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null);

            var controller = new ConferenceController(context, userManager);


        }


    }
}