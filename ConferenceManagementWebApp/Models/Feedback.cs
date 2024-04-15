using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.Models;

public class Feedback
{
    [Key]
    [Required]
    public string Id { get; set; }

    [Required]
    public int Rating { get; set; }

    [StringLength(500)]
    public string? Comment { get; set; }

    [Required]
    public ApplicationUser Attendee { get; set; }

    [Required]
    public Conference Conference { get; set; }
}
