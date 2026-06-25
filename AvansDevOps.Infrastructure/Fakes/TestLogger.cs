using System.Collections.Generic;

namespace AvansDevOps.Infrastructure.Fakes;

/// <summary>
/// Simple logging interface for tracking object creation and operations.
/// </summary>
public interface ITestLogger
{
    void Log(string message);
    void LogCreated(string objectType, string objectId, string details);
    void LogAction(string action, string details);
    void LogError(string error);
    IReadOnlyList<string> GetLogs();
    void Clear();
}

/// <summary>
/// Simple in-memory logger implementation for testing and debugging.
/// </summary>
public class ConsoleTestLogger : ITestLogger
{
    private readonly List<string> _logs = [];

    public void Log(string message)
    {
        var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
        var logEntry = $"[{timestamp}] {message}";
        _logs.Add(logEntry);
        Console.WriteLine(logEntry);
    }

    public void LogCreated(string objectType, string objectId, string details)
    {
        var message = $"✓ CREATED {objectType} | ID: {objectId} | {details}";
        Log(message);
    }

    public void LogAction(string action, string details)
    {
        var message = $"→ ACTION: {action} | {details}";
        Log(message);
    }

    public void LogError(string error)
    {
        var message = $"✗ ERROR: {error}";
        Log(message);
    }

    public IReadOnlyList<string> GetLogs()
    {
        return _logs.AsReadOnly();
    }

    public void Clear()
    {
        _logs.Clear();
    }
}
