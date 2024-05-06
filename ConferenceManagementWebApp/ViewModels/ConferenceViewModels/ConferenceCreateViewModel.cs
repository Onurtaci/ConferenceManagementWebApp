using ConferenceManagementWebApp.Enums;
using ConferenceManagementWebApp.Models;
using ConferenceManagementWebApp.ViewModels.SessionViewModels;

namespace ConferenceManagementWebApp.ViewModels.ConferenceViewModels;

public class ConferenceCreateViewModel
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string Venue { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public List<ApplicationUser> AllReviewers { get; set; }

    public List<string> SelectedReviewers { get; set; }

    public List<string> SessionsData { get; set; }

    public List<ApplicationUser> AllPresenters { get; set; }

}
