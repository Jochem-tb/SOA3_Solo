using AvansDevOps.Domain.Entities;

namespace AvansDevOps.Infrastructure.Adapters;

/// <summary>
/// Adapter for sending SMS text messages.
/// Uses the Adapter Pattern to bridge the external SMS/Telephony API to our domain.
/// </summary>
/// <remarks>
/// Pattern: Adapter Pattern
/// This adapter wraps an external SMS service and provides a simple interface.
/// In a real application, this would integrate with Twilio, AWS SNS, or similar.
/// </remarks>
public class SMSAdapter
{
    /// <summary>
    /// Sends an SMS message to a user.
    /// </summary>
    /// <param name="message">The SMS body/message content.</param>
    /// <param name="user">The user to send to (would use user's phone number).</param>
    public void SendSMSMessage(string message, User user)
    {
        // Stub: In production, this would call an external SMS service
        // Example: Twilio, AWS SNS, Nexmo, etc.
        
        if (user != null)
        {
            Console.WriteLine($"📱 [SMS ADAPTER] Sending SMS message to: {user.Name}");
            Console.WriteLine($"   Message: {message}");
        }
    }
}
