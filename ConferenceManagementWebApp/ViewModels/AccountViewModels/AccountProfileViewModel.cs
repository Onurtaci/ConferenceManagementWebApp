using ConferenceManagementWebApp.Constants;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.ViewModels.AccountViewModels;

public class AccountProfileViewModel
{
    [Required]
    public string Id { get; set; }

    [Required (ErrorMessage = Messages.FirstNameRequired)]
    [StringLength(50, ErrorMessage = Messages.FirstNameMaxLength)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = Messages.LastNameRequired)]
    [StringLength(50, ErrorMessage = Messages.LastNameMaxLength)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "User Name")]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
