using ConferenceManagementWebApp.Models;

namespace ConferenceManagementWebApp.ViewModels.ConferenceViewModels;

public class ConferenceListViewModel
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Venue { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string OrganizerFullName { get; set; }
    public List<Session> Sessions { get; set; }
}
