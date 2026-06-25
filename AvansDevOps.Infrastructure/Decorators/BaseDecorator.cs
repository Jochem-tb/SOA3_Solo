using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Infrastructure.Decorators;

/// <summary>
/// Base decorator for the Notifier interface.
/// Implements the Decorator Pattern to allow dynamic addition of notification responsibilities.
/// Subclasses (EmailDecorator, SlackDecorator, SMSDecorator) add specific behavior.
/// </summary>
/// <remarks>
/// Pattern: Decorator Pattern
/// This class wraps another INotifier and delegates to it, allowing a chain of decorators.
/// Each subclass adds a specific notification method while preserving the core interface.
/// </remarks>
public abstract class BaseDecorator : INotifier
{
    /// <summary>
    /// The wrapped notifier - can be EmptyNotifier or another decorator.
    /// </summary>
    protected INotifier Wrappee { get; set; }

    /// <summary>
    /// Initializes the decorator with a wrapped notifier.
    /// </summary>
    /// <param name="notifier">The notifier to wrap. If null, uses EmptyNotifier.</param>
    protected BaseDecorator(INotifier? notifier = null)
    {
        Wrappee = notifier ?? new EmptyNotifier();
    }

    /// <summary>
    /// Delegates the send call to the wrapped notifier.
    /// Subclasses override this to add their own notification logic before/after delegating.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="user">The user to notify.</param>
    public virtual void Send(string message, User user)
    {
        // Delegate to the wrapped notifier
        Wrappee.Send(message, user);
    }
}
