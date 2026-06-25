using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Enums;
using AvansDevOps.Domain.Interfaces;
using AvansDevOps.Infrastructure.Decorators;

namespace AvansDevOps.Infrastructure.Fakes;

/// <summary>
/// Extension methods for TestDataFactory to support notification system testing.
/// </summary>
public static class NotificationTestDataFactoryExtensions
{
    private static ITestLogger? _logger;

    /// <summary>
    /// Sets the logger for notification factory operations.
    /// </summary>
    public static void SetLoggerForNotifications(this TestDataFactory factory, ITestLogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Creates a notification manager and tests it with a user.
    /// </summary>
    public static void TestNotificationChain(this TestDataFactory factory, User user, string testMessage)
    {
        _logger?.LogAction("CreateNotificationChain", $"User: {user.Name}, Preferences: {string.Join(", ", user.NotificationPreferences)}");
        
        var notificationManager = new NotificationManager();
        var notifier = notificationManager.CreateNotificationChain(user);

        _logger?.LogAction("SendNotification", $"Sending message to {user.Name}: {testMessage}");
        notifier.Send(testMessage, user);
    }

    /// <summary>
    /// Tests the full notification system with multiple users.
    /// </summary>
    public static void TestFullNotificationSystem(this TestDataFactory factory)
    {
        _logger?.Log("\n=== TESTING NOTIFICATION SYSTEM ===");

        // Create users with different preferences
        var userEmailOnly = factory.CreateUserWithPreferences("Alice (Email Only)", NotificationPreference.Email);
        var userSlackOnly = factory.CreateUserWithPreferences("Bob (Slack Only)", NotificationPreference.Slack);
        var userMultiple = factory.CreateUserWithPreferences("Carol (Email+Slack+SMS)", 
            NotificationPreference.Email, 
            NotificationPreference.Slack, 
            NotificationPreference.SMS);
        var userNoPrefs = factory.CreateUser("Dave (No Preferences)");

        _logger?.Log("\n--- Test 1: Email Only ---");
        factory.TestNotificationChain(userEmailOnly, "Important update for your project!");

        _logger?.Log("\n--- Test 2: Slack Only ---");
        factory.TestNotificationChain(userSlackOnly, "Your sprint is starting soon!");

        _logger?.Log("\n--- Test 3: Multiple Channels ---");
        factory.TestNotificationChain(userMultiple, "Urgent: Production issue detected!");

        _logger?.Log("\n--- Test 4: No Preferences (Empty Notifier) ---");
        factory.TestNotificationChain(userNoPrefs, "This should not be sent anywhere.");

        _logger?.Log("\n=== NOTIFICATION SYSTEM TEST COMPLETE ===");
    }
}
