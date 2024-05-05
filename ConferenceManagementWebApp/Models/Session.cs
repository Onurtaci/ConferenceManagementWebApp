using ConferenceManagementWebApp.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagementWebApp.Models;

public class Session
{
    [Key]
    [Required]
    public string Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Title { get; set; }

    [Required]
    [StringLength(50)]
    public string Topic { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    [StringLength(50)]
    public PresentationTypes PresentationType { get; set; }

    [Required]
    public ApplicationUser Presenter { get; set; }

    [Required]
    public Conference Conference { get; set; }

    public List<Paper> Papers { get; set; } = [];
}
