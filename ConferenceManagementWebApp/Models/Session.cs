using ConferenceManagementWebApp.Constants;
using ConferenceManagementWebApp.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.Models;

public class Session
{
    [Key]
    [Required]
    public string Id { get; set; }

    [Required(ErrorMessage = Messages.TitleRequired)]
    [StringLength(50, ErrorMessage = Messages.TitleMaxLength)]
    public string Title { get; set; }

    [Required(ErrorMessage = Messages.TopicRequired)]
    [StringLength(50, ErrorMessage = Messages.TopicMaxLength)]
    public string Topic { get; set; }

    [Required(ErrorMessage = Messages.SessionStartTimeRequired)]
    [DataType(DataType.DateTime)]
    [Display (Name = "Start Time")]
    public DateTime StartTime { get; set; }

    [Required(ErrorMessage = Messages.SessionEndTimeRequired)]
    [DataType(DataType.DateTime)]
    [Display(Name = "End Time")]
    [Compare(nameof(StartTime), ErrorMessage = Messages.SessionStartTimeBeforeEndTime)]
    public DateTime EndTime { get; set; }

    [Required (ErrorMessage = Messages.PresentationTypeRequired)]
    [EnumDataType(typeof(PresentationTypes), ErrorMessage = Messages.PresentationTypeInvalid)]
    [Display(Name = "Presentation Type")]
    public PresentationTypes PresentationType { get; set; }

    [Required]
    public ApplicationUser Presenter { get; set; }

    [Required]
    public Conference Conference { get; set; }

    public List<Paper> Papers { get; set; } = [];
}
