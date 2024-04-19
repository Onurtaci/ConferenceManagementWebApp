namespace ConferenceManagementWebApp.Models;

public class Notification
{
    public string Id { get; set; }

    public string UserId { get; set; }

    public string Message { get; set; }

    public DateTime CreationDate { get; set; }

    public ApplicationUser User { get; set; }
}
