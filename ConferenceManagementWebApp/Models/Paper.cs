using ConferenceManagementWebApp.Constants;
using ConferenceManagementWebApp.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceManagementWebApp.Models;

public class Paper
{
    [Key]
    [Required]
    public string Id { get; set; }

    [Required(ErrorMessage = Messages.TitleRequired)]
    [StringLength(50, ErrorMessage = Messages.TitleMaxLength)]
    public string Title { get; set; }

    [Required(ErrorMessage = Messages.AbstractRequired)]
    [StringLength(50, ErrorMessage = Messages.AbstractMaxLength)]
    public string Abstract { get; set; }

    [Required(ErrorMessage = Messages.KeywordsRequired)]
    [StringLength(50, ErrorMessage = Messages.KeywordsMaxLength)]
    public string Keywords { get; set; }

    [NotMapped] // db will not contain File
    public IFormFile File { get; set; }

    [Required(ErrorMessage = Messages.FileRequired)]
    public byte[] FileBytes { get; set; }

    [Required]
    [EnumDataType(typeof(Status), ErrorMessage = Messages.StatusInvalid)]
    public Status Status { get; set; }

    public Session Session { get; set; }

    public ApplicationUser Author { get; set; }

    public Review Review { get; set; }
}
