using ConferenceManagementWebApp.Constants;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.ViewModels.AccountViewModels;

public class AccountRegisterViewModel
{
    [Required(ErrorMessage = Messages.FirstNameRequired)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = Messages.LastNameRequired)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required(ErrorMessage = Messages.UsernameRequired)]
    [Display(Name = "Username")]
    public string Username { get; set; }

    [Required(ErrorMessage = Messages.EmailRequired)]
    public string Email { get; set; }

    [Required(ErrorMessage = Messages.PasswordSpecification)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = Messages.PasswordSpecification)]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = Messages.PasswordsDoNotMatch)]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = Messages.RoleRequired)]
    public string Role { get; set; }
}
