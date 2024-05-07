using ConferenceManagementWebApp.Constants;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.Models;

public class Notification
{
    public string Id { get; set; }

    [Required(ErrorMessage = "Message is required.")]
    [Range(1, 500, ErrorMessage = Messages.MessageMaxLength)]
    public string Message { get; set; }

    [Required(ErrorMessage = "Creation date is required.")]
    [DataType(DataType.DateTime)]
    [Display(Name = "Creation Date")]
    public DateTime CreationDate { get; set; }

    public ApplicationUser User { get; set; }
}
