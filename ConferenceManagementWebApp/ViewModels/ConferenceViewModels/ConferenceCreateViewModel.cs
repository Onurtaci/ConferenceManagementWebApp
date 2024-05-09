using ConferenceManagementWebApp.Constants;
using ConferenceManagementWebApp.Enums;
using ConferenceManagementWebApp.Models;
using ConferenceManagementWebApp.ViewModels.SessionViewModels;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.ViewModels.ConferenceViewModels;

public class ConferenceCreateViewModel
{
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

    [Required (ErrorMessage = Messages.ReviewersDoNotSelected)]
    public List<ApplicationUser> AllReviewers { get; set; }

    public List<string> SelectedReviewers { get; set; }

    public List<string> SessionsData { get; set; }

    public List<ApplicationUser> AllPresenters { get; set; }

}
