using AvansDevOps.Domain.Enums;

namespace AvansDevOps.Domain.Entities;

/// <summary>
/// Represents a user in the system.
/// Can have notification preferences and be assigned to work items.
/// </summary>
public class User
{
    public string Name { get; set; } = string.Empty;
    public List<NotificationPreference> NotificationPreferences { get; set; } = [];
}
