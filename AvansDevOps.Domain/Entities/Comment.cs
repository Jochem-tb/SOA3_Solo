namespace AvansDevOps.Domain.Entities;

/// <summary>
/// Represents a comment in a discussion thread.
/// </summary>
public class Comment
{
    public DateTime Time { get; set; }
    public User? User { get; set; }
    public string Message { get; set; } = string.Empty;
}
