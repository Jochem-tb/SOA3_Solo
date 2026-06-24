namespace AvansDevOps.Domain.Entities;

/// <summary>
/// Represents a discussion thread for a backlog item.
/// Contains a list of comments/messages.
/// </summary>
public class Discussion
{
    public List<Comment> Messages { get; set; } = [];
}
