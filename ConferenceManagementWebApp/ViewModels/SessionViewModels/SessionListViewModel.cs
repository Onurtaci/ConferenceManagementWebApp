using ConferenceManagementWebApp.Constants;
using ConferenceManagementWebApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.ViewModels.SessionViewModels;

public class SessionListViewModel
{
    [Required(ErrorMessage = Messages.SessionIdRequired)]
    public string SessionId { get; set; }

    [Required(ErrorMessage = Messages.TitleRequired)]
    [MaxLength(50, ErrorMessage = Messages.TitleMaxLength)]
    public string Title { get; set; }

    [Required(ErrorMessage = Messages.TopicRequired)]
    [MaxLength(50, ErrorMessage = Messages.TopicMaxLength)]
    public string Topic { get; set; }

    [Required(ErrorMessage = Messages.PresentationTypeRequired)]
    [EnumDataType(typeof(PresentationTypes), ErrorMessage = Messages.PresentationTypeInvalid)]
    [Display(Name = "Presentation Type")]
    public PresentationTypes PresentationType { get; set; }

    [Required(ErrorMessage = Messages.PresenterRequired)]
    [Display(Name = "Presenter")]
    public string PresenterFullName { get; set; }

    [Required(ErrorMessage = Messages.SessionStartTimeRequired)]
    [Display(Name = "Start Time")]
    public DateTime StartTime { get; set; }

    [Required(ErrorMessage = Messages.SessionEndTimeRequired)]
    [Display(Name = "End Time")]
    [Compare(nameof(StartTime), ErrorMessage = Messages.SessionStartTimeBeforeEndTime)]
    public DateTime EndTime { get; set; }
}
