using ConferenceManagementWebApp.Constants;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.ViewModels.AccountViewModels;

public class AccountLoginViewModel
{
    [Required(ErrorMessage = Messages.UsernameRequired)]
    public string Username { get; set; }

    [Required(ErrorMessage = Messages.PasswordRequired)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}
