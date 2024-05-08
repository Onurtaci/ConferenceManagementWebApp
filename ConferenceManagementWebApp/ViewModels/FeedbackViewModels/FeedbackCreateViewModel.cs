using ConferenceManagementWebApp.Constants;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.ViewModels.FeedbackViewModels;

public class FeedbackCreateViewModel
{
    [Required]
    public string ConferenceId { get; set; }

    [Required]
    public string UserId { get; set; }

    [Required]
    [Range(1, 5, ErrorMessage = Messages.RatingRange)]
    public int Rating { get; set; }

    public string Comment { get; set; }
}
