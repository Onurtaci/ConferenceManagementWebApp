using ConferenceManagementWebApp.Constants;
using ConferenceManagementWebApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.ViewModels.PaperViewModels;

public class PaperListReviewedViewModel
{
    [Required(ErrorMessage = Messages.TitleRequired)]
    [StringLength(50, ErrorMessage = Messages.TitleMaxLength)]
    [Display(Name = "Paper Title")]
    public string Title { get; set; }

    [Range(0, 10, ErrorMessage = Messages.ScoreRange)]
    public int? Score { get; set; }

    [StringLength(50, ErrorMessage = Messages.CommentMaxLength)]
    public string Comment { get; set; }

    [Required]
    [EnumDataType(typeof(Recommendation), ErrorMessage = Messages.RecommendationRequired)]
    public Recommendation Recommendation { get; set; }
}
