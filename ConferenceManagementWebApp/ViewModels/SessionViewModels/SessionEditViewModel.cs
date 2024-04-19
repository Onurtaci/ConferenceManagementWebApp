using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.ViewModels.SessionViewModels;

public class SessionEditViewModel
{
    [Required]
    public string Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Topic { get; set; }

    [Required]
    public string PresentationType { get; set; }

    [Required]
    public string PresenterId { get; set; }

    [Required]
    public string ConferenceId { get; set; }
}
