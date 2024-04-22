﻿namespace ConferenceManagementWebApp.ViewModels.ConferenceViewModels;

public class ConferenceCreateViewModel
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string Venue { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}