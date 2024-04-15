using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.Models;

public class Conference
{
    [Key]
    [Required]
    public string Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Title { get; set; }

    [DataType(DataType.MultilineText)]
    [StringLength(500)]
    public string Description { get; set; }

    [Required]
    [StringLength(50)]
    public string Venue { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    public ApplicationUser Organizer { get; set; }

    public ICollection<Session> Sessions { get; set; }

    public ICollection<Feedback> Feedbacks { get; set; }
}
