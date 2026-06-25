using AvansDevOps.Domain.Entities;

namespace AvansDevOps.Infrastructure.Adapters;

/// <summary>
/// Adapter for sending messages through Slack.
/// Uses the Adapter Pattern to bridge the external Slack API to our domain.
/// </summary>
/// <remarks>
/// Pattern: Adapter Pattern
/// This adapter wraps the Slack API and provides a simple interface for sending messages.
/// In a real application, this would call the Slack Webhook API or Bot API.
/// </remarks>
public class SlackAdapter
{
    /// <summary>
    /// Sends a Slack message to a user.
    /// </summary>
    /// <param name="message">The message content to send.</param>
    /// <param name="user">The user to send to (would use user's Slack handle or channel).</param>
    public void SendSlackMessage(string message, User user)
    {
        // Stub: In production, this would call the Slack API
        // Example: POST to Slack Webhook URL or use Slack Bot API
        
        if (user != null)
        {
            Console.WriteLine($"💬 [SLACK ADAPTER] Sending Slack message to: {user.Name}");
            Console.WriteLine($"   Message: {message}");
        }
    }
}
