using ConferenceManagementWebApp.Constants;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.Models;

public class Conference
{
    [Key]
    [Required]
    public string Id { get; set; }

    [Required(ErrorMessage = Messages.TitleRequired)]
    [StringLength(50, ErrorMessage = Messages.TitleMaxLength)]
    public string Title { get; set; }

    [Required(ErrorMessage = Messages.DescriptionRequired)]
    [DataType(DataType.MultilineText)]
    [StringLength(500, ErrorMessage = Messages.DescriptionMaxLength)]
    public string Description { get; set; }

    [Required(ErrorMessage = Messages.VenueRequired)]
    [StringLength(50, ErrorMessage = Messages.VenueMaxLength)]
    public string Venue { get; set; }

    [Required(ErrorMessage = Messages.StartDateRequired)]
    [DataType(DataType.DateTime)]
    [Display (Name = "Start Date")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = Messages.EndDateRequired)]
    [DataType(DataType.DateTime)]
    [Display (Name = "End Date")]
    [Compare(nameof(StartDate), ErrorMessage = Messages.StartDateBeforeEndDate)]
    public DateTime EndDate { get; set; }

    [Required]
    public ApplicationUser Organizer { get; set; }

    public List<Session> Sessions { get; set; } = [];

    public List<Feedback> Feedbacks { get; set; } = [];

    public List<ConferenceAttendee> ConferenceAttendees { get; set; } = [];

    public List<ConferenceReviewer> ConferenceReviewers { get; set; } = [];
}
