using ConferenceManagementWebApp.Constants;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.ViewModels.ConferenceViewModels;

public class ConferenceFeedbackViewModel
{
    public string ConferenceId { get; set; }

    [Required(ErrorMessage = Messages.RatingRequired)]
    [Range(0, 5, ErrorMessage = Messages.RatingRange)]
    public int Rating { get; set; }

    [DataType(DataType.MultilineText)]
    [StringLength(500, ErrorMessage = Messages.CommentMaxLength)]
    public string Comment { get; set; }
}
