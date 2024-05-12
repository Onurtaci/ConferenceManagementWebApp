namespace ConferenceManagementWebApp.Constants;

public static class Messages
{
    #region Conference
    public const string ConferenceCreated = "Conference created successfully!";
    public const string ConferenceUpdated = "Conference updated successfully!";
    public const string ConferenceDeleted = "Conference deleted successfully!";
    public const string ConferenceNotFound = "Conference not found!";
    public const string ConferenceAlreadyExists = "Conference already exists!";
    public const string ConferenceNotCreated = "Conference not created!";
    public const string ConferenceNotUpdated = "Conference not updated!";
    public const string ConferenceNotDeleted = "Conference not deleted!";
    public const string ConferenceEnded = "Conference has ended!";
    public const string JoinConference = "You have joined the conference!";
    public const string LeaveConference = "You have left the conference!";
    public const string AlreadyJoined = "You have already joined the conference!";
    public const string StartDateRequired = "Select a start date!";
    public const string EndDateRequired = "Select an end date!";
    public const string StartDateBeforeEndDate = "Start date must be before end date!";
    #endregion

    #region Session
    public const string SessionCreated = "Session created successfully!";
    public const string SessionUpdated = "Session updated successfully!";
    public const string SessionDeleted = "Session deleted successfully!";
    public const string SessionNotFound = "Session not found!";
    public const string SessionAlreadyExists = "Session already exists!";
    public const string SessionNotCreated = "Session not created!";
    public const string SessionNotUpdated = "Session not updated!";
    public const string SessionNotDeleted = "Session not deleted!";
    public const string SessionStartTimeRequired = "Select a start time!";
    public const string SessionEndTimeRequired = "Select an end time!";
    public const string SessionStartTimeBeforeEndTime = "Start time must be before end time!";
    public const string SessionStartTimeAfterConferenceStart = "Start time must be after conference start time!";
    public const string SessionEndTimeBeforeConferenceEnd = "End time must be before conference end time!";
    #endregion

    #region Feedback
    public const string FeedbackCreated = "Feedback created successfully!";
    public const string FeedbackUpdated = "Feedback updated successfully!";
    public const string FeedbackDeleted = "Feedback deleted successfully!";
    #endregion

    #region Paper
    public const string PaperUploaded = "Paper uploaded successfully!";
    #endregion

    #region Input Validation
    public const string UsernameRequired = "Enter a username!";
    public const string EmailRequired = "Enter an email!";
    public const string PasswordRequired = "Enter a password!";
    public const string PasswordSpecification = "Password must contain at least one big letter, one small letter, one number, and one special character, and must be at least 8 characters long!";
    public const string PasswordsDoNotMatch = "Passwords do not match!";
    public const string TitleRequired = "Enter a title!";
    public const string TopicRequired = "Enter a topic!";
    public const string DescriptionRequired = "Enter a description!";
    public const string RatingRequired = "Enter a rating!";
    public const string PresentationTypeRequired = "Select a presentation type! It can be 'Oral', 'Poster', 'Workshop', Other";
    public const string RecommendationRequired = "Select a recommendation! It can be 'Accept', 'Revise', 'Reject'";
    public const string RoleRequired = "Select a role! It can be 'Organizer', 'Presenter', 'Reviewer', 'Attendee'";
    public const string VenueRequired = "Enter a venue! For example, 'Online' or 'Room 101'";
    public const string AbstractRequired = "Enter an abstract! For example, 'This session will cover...'";
    public const string KeywordsRequired = "Enter keywords! For example, 'Deep Learning, GAN'";
    public const string FileRequired = "Upload a file!";
    public const string OrganizerRequired = "Organizer name is required!";
    public const string PresenterRequired = "Presenter name is required!";
    public const string SessionIdRequired = "Session ID is required!";
    public const string PaperIdRequired = "Paper ID is required!";
    public const string ScoreRange = "Score must be between 0 and 10!";
    public const string RatingRange = "Rating must be between 0 and 5!";
    public const string CommentMaxLength = "Comment must be less than 500 characters!";
    public const string MessageMaxLength = "Message must be less than 500 characters!";
    public const string TitleMaxLength = "Title must be less than 50 characters!";
    public const string DescriptionMaxLength = "Description must be less than 500 characters!";
    public const string VenueMaxLength = "Venue must be less than 50 characters!";
    public const string TopicMaxLength = "Topic must be less than 50 characters!";
    public const string AbstractMaxLength = "Abstract must be less than 500 characters!";
    public const string KeywordsMaxLength = "Keywords must be less than 50 characters!";
    public const string FirstNameMaxLength = "First name must be less than 100 characters!";
    public const string LastNameMaxLength = "Last name must be less than 100 characters!";
    public const string FirstNameRequired = "Enter your first name!";
    public const string LastNameRequired = "Enter your last name!";
    public const string PresentationTypeInvalid = "Invalid presentation type! It can be 'Oral', 'Poster', 'Workshop', Other";
    public const string RecommendationInvalid = "Invalid recommendation! It can be 'Accept', 'Revise', 'Reject'";
    public const string RoleInvalid = "Invalid role! It can be 'Organizer', 'Presenter', 'Reviewer', 'Attendee'";
    public const string StatusInvalid = "Invalid status! It can be 'Submitted', 'Under_Review', 'Accepted', 'Rejected'";
    public const string SessionTypeInvalid = "Invalid session type! It can be 'Oral', 'Poster', 'Workshop', Other";
    public const string LoginAttemptInvalid = "Invalid login attempt!";
    public const string ReviewersDoNotSelected = "Reviewers not selected!";
    public const string FileTypeInvalid = "Invalid file type! It must be a PDF file!";
    #endregion

    #region Review
    public const string ReviewCreated = "Review created successfully!";
    public const string ReviewUpdated = "Review updated successfully!";
    public const string ReviewDeleted = "Review deleted successfully!";
    public const string ReviewNotFound = "Review not found!";
    public const string ReviewAlreadyExists = "Review already exists!";
    public const string ReviewNotCreated = "Review not created!";
    public const string ReviewNotUpdated = "Review not updated!";
    public const string ReviewNotDeleted = "Review not deleted!";
    #endregion
}

