using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Infrastructure.Decorators;

/// <summary>
/// Represents the Null Object pattern implementation for the Notifier.
/// This is the terminal decorator that does nothing when send() is called.
/// It prevents null reference exceptions and provides a safe default.
/// </summary>
/// <remarks>
/// Pattern: Null Object Pattern
/// This is the end of the decorator chain. All other decorators wrap this or each other,
/// and EmptyNotifier represents the base case where no actual notification occurs.
/// </remarks>
public class EmptyNotifier : INotifier
{
    /// <summary>
    /// Sends nothing - this is the null object implementation.
    /// Override this in decorators to add actual notification logic.
    /// </summary>
    /// <param name="message">The message to send (ignored).</param>
    /// <param name="user">The user to notify (ignored).</param>
    public void Send(string message, User user)
    {
        // Intentionally empty - Null Object pattern
        // This prevents NullReferenceException and serves as the base case
    }
}
