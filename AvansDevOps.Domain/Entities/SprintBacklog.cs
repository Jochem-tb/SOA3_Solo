namespace AvansDevOps.Domain.Entities;

/// <summary>
/// Represents the backlog for a specific sprint.
/// Contains backlog items assigned to a sprint.
/// </summary>
public class SprintBacklog
{
    /// <summary>
    /// Unique identifier for the sprint backlog.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Collection of backlog items assigned to this sprint.
    /// </summary>
    public List<BacklogItem> Items { get; set; } = [];

    /// <summary>
    /// Adds a backlog item to the sprint backlog.
    /// </summary>
    /// <param name="item">The backlog item to add.</param>
    public void AddItem(BacklogItem item)
    {
        if (item != null && !Items.Contains(item))
        {
            Items.Add(item);
        }
    }

    /// <summary>
    /// Removes a backlog item from the sprint backlog.
    /// </summary>
    /// <param name="item">The backlog item to remove.</param>
    public void RemoveItem(BacklogItem item)
    {
        Items.Remove(item);
    }

    /// <summary>
    /// Checks if all items in the sprint backlog are done.
    /// </summary>
    /// <returns>True if all items are done, false otherwise.</returns>
    public bool IsDone()
    {
        return Items.Count > 0 && Items.All(item => item.IsDone());
    }
}
