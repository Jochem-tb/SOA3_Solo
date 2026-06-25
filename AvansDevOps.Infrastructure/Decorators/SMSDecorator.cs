using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;
using AvansDevOps.Infrastructure.Adapters;

namespace AvansDevOps.Infrastructure.Decorators;

/// <summary>
/// SMS notification decorator.
/// Adds SMS notification capability to the notifier chain using the SMS Adapter.
/// </summary>
/// <remarks>
/// Pattern: Decorator Pattern
/// This decorator wraps another INotifier and adds SMS notification functionality.
/// It uses the SMSAdapter to send the actual SMS through an external service.
/// </remarks>
public class SMSDecorator : BaseDecorator
{
    private readonly SMSAdapter _smsAdapter;

    /// <summary>
    /// Initializes the SMS decorator with an SMS adapter and wrapped notifier.
    /// </summary>
    /// <param name="smsAdapter">The adapter for sending SMS messages.</param>
    /// <param name="wrappee">The wrapped notifier (next in chain or EmptyNotifier).</param>
    public SMSDecorator(SMSAdapter smsAdapter, INotifier? wrappee = null)
        : base(wrappee)
    {
        _smsAdapter = smsAdapter;
    }

    /// <summary>
    /// Sends notification via SMS and delegates to wrapped notifier.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="user">The user to notify.</param>
    public override void Send(string message, User user)
    {
        // Send SMS first
        _smsAdapter.SendSMSMessage(message, user);

        // Then delegate to wrapped notifier (next in chain)
        base.Send(message, user);
    }
}
