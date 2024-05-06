using ConferenceManagementWebApp.Enums;

namespace ConferenceManagementWebApp.ViewModels.SessionViewModels;

public class SessionListViewModel
{
    public string SessionId { get; set; }
    public string Title { get; set; }
    public string Topic { get; set; }
    public PresentationTypes PresentationType { get; set; }
    public string PresenterFullName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
