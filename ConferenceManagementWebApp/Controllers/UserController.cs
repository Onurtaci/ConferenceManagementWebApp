using ConferenceManagementWebApp.Models;
using ConferenceManagementWebApp.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManagementWebApp.Controllers;

public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersByRole(string roleName)
    {
        if (string.IsNullOrEmpty(roleName))
        {
            return BadRequest("Role name cannot be null or empty.");
        }

        try
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);
            return Ok(users); // Return users with 200 OK status
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while retrieving users.");
        }
    }
}
