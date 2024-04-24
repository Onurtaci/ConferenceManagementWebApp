using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.Models;

public class ConferenceAttendee
{
    [Key]
    [Required]
    public string Id { get; set; }

    [Required]
    public string ConferenceId { get; set; }

    [Required]
    public string AttendeeId { get; set; }

    [Required]
    public Conference Conference { get; set; }

    [Required]
    public ApplicationUser Attendee { get; set; }
}
