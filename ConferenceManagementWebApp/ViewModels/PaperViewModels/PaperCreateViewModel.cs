using ConferenceManagementWebApp.Constants;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.ViewModels.PaperViewModels;

public class PaperCreateViewModel
{
    [Required (ErrorMessage = Messages.TitleRequired)]
    [StringLength(50, ErrorMessage = Messages.TitleMaxLength)]
    public string Title { get; set; }

    [Required (ErrorMessage = Messages.AbstractRequired)]
    [StringLength(50, ErrorMessage = Messages.AbstractMaxLength)]
    public string Abstract { get; set; }

    [Required (ErrorMessage = Messages.KeywordsRequired)]
    [StringLength(50, ErrorMessage = Messages.KeywordsMaxLength)]
    public string Keywords { get; set; }

    [Required (ErrorMessage = Messages.FileRequired)]
    public IFormFile File { get; set; }

    [Required]
    public string SessionId { get; set; }
}
