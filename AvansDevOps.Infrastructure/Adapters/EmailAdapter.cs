using AvansDevOps.Domain.Entities;

namespace AvansDevOps.Infrastructure.Adapters;

/// <summary>
/// Adapter for sending emails through an external SMTP/Email service.
/// Uses the Adapter Pattern to bridge the external email API to our domain.
/// </summary>
/// <remarks>
/// Pattern: Adapter Pattern
/// This adapter wraps an external email service API and provides a simple interface.
/// In a real application, this would integrate with SendGrid, SMTP, or similar.
/// </remarks>
public class EmailAdapter
{
    /// <summary>
    /// Sends an email message to a user.
    /// </summary>
    /// <param name="message">The email body/message content.</param>
    /// <param name="user">The user to send to (uses email address from user).</param>
    public void SendEmailMessage(string message, User user)
    {
        // Stub: In production, this would call an external email service
        // Example: SendGrid, SMTP Server, AWS SES, etc.
        
        if (user?.Email != null)
        {
            Console.WriteLine($"📧 [EMAIL ADAPTER] Sending email to: {user.Email}");
            Console.WriteLine($"   Message: {message}");
        }
    }
}
