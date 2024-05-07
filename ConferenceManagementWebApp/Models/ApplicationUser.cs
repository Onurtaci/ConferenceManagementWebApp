using ConferenceManagementWebApp.Constants;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.Models;

public class ApplicationUser : IdentityUser
{
    [Required(ErrorMessage = Messages.FirstNameRequired)]
    [StringLength(100, ErrorMessage = Messages.FirstNameMaxLength)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = Messages.LastNameRequired)]
    [StringLength(100, ErrorMessage = Messages.LastNameMaxLength)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
}
