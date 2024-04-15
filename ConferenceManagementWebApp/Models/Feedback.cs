using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.Models;

public class Feedback
{
    [Key]
    [Required]
    public string Id { get; set; }

    [Required]
    public string AttendeeId { get; set; }

    [Required]
    public string ConferenceId { get; set; }

    [Required]
    public int Rate { get; set; }

    [StringLength(500)]
    public string? Comments { get; set; }

    [Required]
    public ApplicationUser Attendee { get; set; }

    [Required]
    public Conference Conference { get; set; }
}
