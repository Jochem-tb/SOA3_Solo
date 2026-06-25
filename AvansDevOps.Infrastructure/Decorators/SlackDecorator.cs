using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;
using AvansDevOps.Infrastructure.Adapters;

namespace AvansDevOps.Infrastructure.Decorators;

/// <summary>
/// Slack notification decorator.
/// Adds Slack notification capability to the notifier chain using the Slack Adapter.
/// </summary>
/// <remarks>
/// Pattern: Decorator Pattern
/// This decorator wraps another INotifier and adds Slack notification functionality.
/// It uses the SlackAdapter to send the actual message through Slack.
/// </remarks>
public class SlackDecorator : BaseDecorator
{
    private readonly SlackAdapter _slackAdapter;

    /// <summary>
    /// Initializes the Slack decorator with a Slack adapter and wrapped notifier.
    /// </summary>
    /// <param name="slackAdapter">The adapter for sending Slack messages.</param>
    /// <param name="wrappee">The wrapped notifier (next in chain or EmptyNotifier).</param>
    public SlackDecorator(SlackAdapter slackAdapter, INotifier? wrappee = null)
        : base(wrappee)
    {
        _slackAdapter = slackAdapter;
    }

    /// <summary>
    /// Sends notification via Slack and delegates to wrapped notifier.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="user">The user to notify.</param>
    public override void Send(string message, User user)
    {
        // Send Slack message first
        _slackAdapter.SendSlackMessage(message, user);

        // Then delegate to wrapped notifier (next in chain)
        base.Send(message, user);
    }
}
