namespace AvansDevOps.Domain.Entities;

/// <summary>
/// Represents the product backlog for a project.
/// Contains all backlog items that are not yet assigned to a sprint.
/// </summary>
public class ProductBacklog
{
    /// <summary>
    /// Unique identifier for the product backlog.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Collection of backlog items in the product backlog.
    /// </summary>
    public List<BacklogItem> Items { get; set; } = [];

    /// <summary>
    /// Adds a backlog item to the product backlog.
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
    /// Removes a backlog item from the product backlog.
    /// </summary>
    /// <param name="item">The backlog item to remove.</param>
    public void RemoveItem(BacklogItem item)
    {
        Items.Remove(item);
    }
}
