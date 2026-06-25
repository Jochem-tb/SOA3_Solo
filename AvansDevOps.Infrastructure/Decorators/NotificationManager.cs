using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Enums;
using AvansDevOps.Domain.Interfaces;
using AvansDevOps.Infrastructure.Adapters;

namespace AvansDevOps.Infrastructure.Decorators;

/// <summary>
/// Manages the creation and configuration of notification chains.
/// Dynamically builds decorator chains based on user notification preferences.
/// </summary>
public class NotificationManager
{
    private readonly EmailAdapter _emailAdapter;
    private readonly SlackAdapter _slackAdapter;
    private readonly SMSAdapter _smsAdapter;

    public NotificationManager()
    {
        _emailAdapter = new EmailAdapter();
        _slackAdapter = new SlackAdapter();
        _smsAdapter = new SMSAdapter();
    }

    /// <summary>
    /// Creates a notification chain based on user's notification preferences.
    /// Builds a decorator chain that sends notifications through all enabled channels.
    /// </summary>
    /// <param name="user">The user whose preferences to use.</param>
    /// <returns>An INotifier configured with the user's preferences.</returns>
    public INotifier CreateNotificationChain(User user)
    {
        // Start with the empty notifier (base case)
        INotifier notifier = new EmptyNotifier();

        // Build the chain based on preferences
        // Order: SMS -> Slack -> Email (outer to inner for proper wrapping)
        if (user.NotificationPreferences.Contains(NotificationPreference.Email))
        {
            notifier = new EmailDecorator(_emailAdapter, notifier);
        }

        if (user.NotificationPreferences.Contains(NotificationPreference.Slack))
        {
            notifier = new SlackDecorator(_slackAdapter, notifier);
        }

        if (user.NotificationPreferences.Contains(NotificationPreference.SMS))
        {
            notifier = new SMSDecorator(_smsAdapter, notifier);
        }

        return notifier;
    }

    /// <summary>
    /// Sends a notification to a user using their configured notification chain.
    /// </summary>
    /// <param name="user">The user to notify.</param>
    /// <param name="message">The message to send.</param>
    public void Notify(User user, string message)
    {
        var notifier = CreateNotificationChain(user);
        notifier.Send(message, user);
    }
}
