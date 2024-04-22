using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.ViewModels.PaperViewModels;

public class PaperCreateViewModel
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Abstract { get; set; }

    [Required]
    public string Keywords { get; set; }

    [Required]
    public IFormFile File { get; set; }

    [Required]
    public string SessionId { get; set; }

    [Required]
    public string AuthorId { get; set; }

    public string? SelectedReviewerId { get; set; }
}
