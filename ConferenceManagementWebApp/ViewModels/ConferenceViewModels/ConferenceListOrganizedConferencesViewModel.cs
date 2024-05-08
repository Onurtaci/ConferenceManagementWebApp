using ConferenceManagementWebApp.Constants;
using ConferenceManagementWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.ViewModels.ConferenceViewModels;

public class ConferenceListOrganizedConferencesViewModel
{
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
    [Display(Name = "Start Date")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = Messages.EndDateRequired)]
    [DataType(DataType.DateTime)]
    [Display(Name = "End Date")]
    public DateTime EndDate { get; set; }

    public List<Session> Sessions { get; set; }

    public List<ApplicationUser> Attendees { get; set; }
}
