using ConferenceManagementWebApp.Constants;
using ConferenceManagementWebApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.Models;

public class Review
{
    [Key]
    [Required]
    public string Id { get; set; }

    [Required (ErrorMessage = Messages.PaperIdRequired)]
    public string PaperId { get; set; }

    [Range(0, 10, ErrorMessage = Messages.ScoreRange)]
    public int? Score { get; set; }

    [EnumDataType(typeof(Recommendation), ErrorMessage = Messages.RecommendationInvalid)]
    public Recommendation? Recommendation { get; set; }

    [Range(0, 500, ErrorMessage = Messages.CommentMaxLength)]
    public string? Comment { get; set; }

    [Required]
    public ApplicationUser Reviewer { get; set; }

    [Required]
    public Paper Paper { get; set; }
}
