using AvansDevOps.Domain.Entities;

namespace AvansDevOps.Domain.Interfaces;

/// <summary>
/// Represents a notifier that can send messages to users.
/// Part of the Decorator Pattern - base interface for notification decorators.
/// Implementations include Email, Slack, SMS, and the Null Object (EmptyNotifier).
/// </summary>
public interface INotifier
{
    /// <summary>
    /// Sends a notification message to a user.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="user">The user to send the notification to.</param>
    void Send(string message, User user);
}
