using AvansDevOps.Domain.Entities;

namespace AvansDevOps.Domain.Interfaces;

/// <summary>
/// Represents an observer that can be notified of changes in observable objects.
/// Part of the Observer Pattern.
/// </summary>
public interface IObserver
{
    /// <summary>
    /// Called when an observable object notifies its observers of a change.
    /// </summary>
    /// <param name="context">Contextual information about the change.</param>
    /// <param name="user">The user associated with the change.</param>
    void Update(string context, User user);
}
