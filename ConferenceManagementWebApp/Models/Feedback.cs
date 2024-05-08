using ConferenceManagementWebApp.Constants;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.Models;

public class Feedback
{
    [Key]
    [Required]
    public string Id { get; set; }

    [Required(ErrorMessage = Messages.RatingRequired)]
    [Range(1, 5, ErrorMessage = Messages.RatingRange)]
    public int Rating { get; set; }

    [StringLength(500, ErrorMessage = Messages.CommentMaxLength)]
    public string? Comment { get; set; }

    [Required]
    public ApplicationUser Attendee { get; set; }

    [Required]
    public Conference Conference { get; set; }
}
