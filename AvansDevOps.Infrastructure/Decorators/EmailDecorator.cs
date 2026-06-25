using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;
using AvansDevOps.Infrastructure.Adapters;

namespace AvansDevOps.Infrastructure.Decorators;

/// <summary>
/// Email notification decorator.
/// Adds email notification capability to the notifier chain using the Email Adapter.
/// </summary>
/// <remarks>
/// Pattern: Decorator Pattern
/// This decorator wraps another INotifier and adds email notification functionality.
/// It uses the EmailAdapter to send the actual email through an external service.
/// </remarks>
public class EmailDecorator : BaseDecorator
{
    private readonly EmailAdapter _emailAdapter;

    /// <summary>
    /// Initializes the email decorator with an email adapter and wrapped notifier.
    /// </summary>
    /// <param name="emailAdapter">The adapter for sending emails.</param>
    /// <param name="wrappee">The wrapped notifier (next in chain or EmptyNotifier).</param>
    public EmailDecorator(EmailAdapter emailAdapter, INotifier? wrappee = null)
        : base(wrappee)
    {
        _emailAdapter = emailAdapter;
    }

    /// <summary>
    /// Sends notification via email and delegates to wrapped notifier.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="user">The user to notify.</param>
    public override void Send(string message, User user)
    {
        // Send email first
        _emailAdapter.SendEmailMessage(message, user);

        // Then delegate to wrapped notifier (next in chain)
        base.Send(message, user);
    }
}
