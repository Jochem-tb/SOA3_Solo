using AvansDevOps.Domain.Enums;
using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Domain.Entities;

/// <summary>
/// Represents a user in the system.
/// Users can have notification preferences and be assigned to work items.
/// </summary>
public class User
{
    /// <summary>
    /// Unique identifier for the user.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The user's name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The user's email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The user's notification preferences.
    /// </summary>
    public List<NotificationPreference> NotificationPreferences { get; set; } = [];
}
