using ConferenceManagementWebApp.Enums;

namespace ConferenceManagementWebApp.ViewModels.SessionViewModels
{
    public class SessionDataViewModel
    {
        public string Title { get; set; }

        public string Topic { get; set; }

        public PresentationTypes PresentationType { get; set; }

        public string PresenterId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
