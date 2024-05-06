using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.Models;

public class Conference
{
    [Key]
    [Required]
    public string Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Title { get; set; }

    [DataType(DataType.MultilineText)]
    [StringLength(500)]
    public string Description { get; set; }

    [Required]
    [StringLength(50)]
    public string Venue { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public ApplicationUser Organizer { get; set; }

    public List<Session> Sessions { get; set; } = [];

    public List<Feedback> Feedbacks { get; set; } = [];

    public List<ConferenceAttendee> ConferenceAttendees { get; set; } = [];

    public List<ConferenceReviewer> ConferenceReviewers { get; set; } = [];
}
