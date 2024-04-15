using ConferenceManagementWebApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.Models;

public class Review
{
    [Key]
    [Required]
    public string Id { get; set; }

    [Required]
    public string ReviewerId { get; set; }

    [Required]
    public string PaperId { get; set; }

    [Range(0, 100, ErrorMessage = "The score must be in the range of 0-100.")]
    public int? Score { get; set; }

    public Recommendation? Recommendation { get; set; }

    public string? Comments { get; set; }

    [Required]
    public ApplicationUser Reviewer { get; set; }

    [Required]
    public Paper Paper { get; set; }


}
