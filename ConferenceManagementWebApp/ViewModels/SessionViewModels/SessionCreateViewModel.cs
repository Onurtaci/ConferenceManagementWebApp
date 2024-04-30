using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.ViewModels.SessionViewModels;

public class SessionCreateViewModel
{    
    [Required]
    public string Title { get; set; }

    [Required]
    public string Topic { get; set; }

    [Required]
    public string PresentationType { get; set; }

    //Start time and end time

    [Required]
    public string PresenterId { get; set; }

    [Required]
    public string ConferenceId { get; set; }
}
