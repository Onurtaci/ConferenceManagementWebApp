using ConferenceManagementWebApp.Constants;
using ConferenceManagementWebApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.ViewModels.PaperViewModels;

public class PaperReviewViewModel
{
    [Required(ErrorMessage = Messages.PaperIdRequired)]
    public string PaperId { get; set; }

    [Required]
    [Range(0, 10, ErrorMessage = Messages.ScoreRange)]
    public string Score { get; set; }

    [DataType(DataType.MultilineText)]
    [StringLength(500, ErrorMessage = Messages.CommentMaxLength)]
    public string Comment { get; set; }

    [Required(ErrorMessage = Messages.RecommendationRequired)]
    [EnumDataType(typeof(Recommendation), ErrorMessage = Messages.RecommendationInvalid)]
    public Recommendation Recommendation { get; set; }
}
