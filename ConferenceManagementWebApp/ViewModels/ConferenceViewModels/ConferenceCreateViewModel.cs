using ConferenceManagementWebApp.Enums;
using ConferenceManagementWebApp.Models;

namespace ConferenceManagementWebApp.ViewModels.ConferenceViewModels;

public class ConferenceCreateViewModel
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string Venue { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public ICollection<ApplicationUser> AllReviewers { get; set; }

    public ICollection<string> SelectedReviewers { get; set; }

    public ICollection<Session> Sessions { get; set; }

    public ICollection<ApplicationUser> AllPresenters { get; set; }

}
