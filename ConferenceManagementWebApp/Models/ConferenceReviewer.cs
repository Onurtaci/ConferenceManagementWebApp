using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.Models;

public class ConferenceReviewer
{
    [Key]
    [Required]
    public string Id { get; set; }

    [Required]
    public string ConferenceId { get; set; }

    [Required]
    public string ReviewerId { get; set; }

    [Required]
    public Conference Conference { get; set; }

    [Required]
    public ApplicationUser Reviewer { get; set; }
}
