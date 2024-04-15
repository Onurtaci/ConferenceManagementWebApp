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
    [StringLength(50)]
    public string PresentationType { get; set; }

    [Required]
    public ApplicationUser Presenter { get; set; }

    [Required]
    public Conference Conference { get; set; }

    public ICollection<Paper> Paper { get; set; }
}
