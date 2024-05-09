using ConferenceManagementWebApp.Controllers;
using ConferenceManagementWebApp.Models;
using ConferenceManagementWebApp.ViewModels.AccountViewModels;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceManagementWebAppTests.UnitTests;

public class AccountUnitTests : IDisposable
{
    private SignInManager<ApplicationUser> _signInManager;
    private UserManager<ApplicationUser> _userManager;
    private RoleManager<IdentityRole> _roleManager;
    private AccountController _accountController;

    [TearDown]
    public void Dispose()
    {
        _accountController = null;
        _signInManager = null;
        _userManager = null;
        _roleManager = null;
    }

    [SetUp]
    public void Setup()
    {
        _signInManager = A.Fake<SignInManager<ApplicationUser>>();
        _userManager = A.Fake<UserManager<ApplicationUser>>();
        _roleManager = A.Fake<RoleManager<IdentityRole>>();
        _accountController = new AccountController(_signInManager, _userManager, _roleManager);
    }

    [Test]
    public async Task Login_WithValidModel_ReturnsRedirectToActionResult()
    {
        // Arrange
        var model = new AccountLoginViewModel
        {
            Username = "testuser",
            Password = "password",
            RememberMe = false
        };

        A.CallTo(() => _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false))
            .Returns(Microsoft.AspNetCore.Identity.SignInResult.Success);

        // Act
        var result = await _accountController.Login(model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
    }

    [Test]
    public async Task Login_WithInvalidModel_ReturnsViewResult()
    {
        // Arrange
        var model = new AccountLoginViewModel
        {
            Username = "testuser",
            Password = "password",
            RememberMe = false
        };

        A.CallTo(() => _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false))
            .Returns(Microsoft.AspNetCore.Identity.SignInResult.Failed);

        // Act
        var result = await _accountController.Login(model);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
    }
}
