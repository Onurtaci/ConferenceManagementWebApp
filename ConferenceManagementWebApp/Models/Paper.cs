using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceManagementWebApp.Models;

public enum Status
{
    Submitted,
    Under_Review,
    Accepted,
    Rejected
}

public class Paper
{
    [Key]
    [Required]
    public string Id { get; set; }

    [Required]
    public string SessionId { get; set; }

    [Required]
    public string AuthorId { get; set; }

    [Required]
    [StringLength(50)]
    public string Title { get; set; }

    [StringLength(50)]
    public string Abstract { get; set; }

    [StringLength(50)]
    public string Keywords { get; set; }

    [NotMapped] // db will not contain File
    public IFormFile File { get; set; }

    [Required]
    public byte[] FileBytes { get; set; }

    public Status Status { get; set; }

    public Session Session { get; set; }

    public ApplicationUser Author { get; set; }

    public Review? Review { get; set; } // maybe will changed this code
}
