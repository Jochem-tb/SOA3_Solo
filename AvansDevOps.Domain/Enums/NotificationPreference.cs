namespace AvansDevOps.Domain.Enums;

/// <summary>
/// Defines the notification preferences available to users.
/// </summary>
public enum NotificationPreference
{
    /// <summary>
    /// Send notifications via Slack.
    /// </summary>
    Slack,

    /// <summary>
    /// Send notifications via SMS.
    /// </summary>
    SMS,

    /// <summary>
    /// Send notifications via Email.
    /// </summary>
    Email
}
