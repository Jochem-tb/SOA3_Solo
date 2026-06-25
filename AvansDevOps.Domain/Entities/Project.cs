using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Domain.Entities;

/// <summary>
/// Represents a project in the system.
/// A project contains a product backlog and multiple sprints.
/// </summary>
public class Project
{
    /// <summary>
    /// Unique identifier for the project.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The name of the project.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Optional description of the project.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The source control system associated with this project.
    /// Implements the Adapter Pattern to bridge external VCS APIs.
    /// </summary>
    public ISourceControl? Repository { get; set; }

    /// <summary>
    /// The product backlog containing all items for this project.
    /// </summary>
    public ProductBacklog ProductBacklog { get; set; } = new();

    /// <summary>
    /// Collection of sprints in this project.
    /// </summary>
    public List<Sprint> Sprints { get; set; } = [];

    /// <summary>
    /// The product owner of this project.
    /// </summary>
    public User? ProductOwner { get; set; }

    /// <summary>
    /// Adds a sprint to the project.
    /// </summary>
    /// <param name="sprint">The sprint to add.</param>
    public void AddSprint(Sprint sprint)
    {
        if (sprint == null)
        {
            throw new ArgumentNullException(nameof(sprint));
        }

        if (!Sprints.Contains(sprint))
        {
            Sprints.Add(sprint);
        }
    }

    /// <summary>
    /// Adds a backlog item to the project's product backlog.
    /// </summary>
    /// <param name="item">The backlog item to add.</param>
    public void AddBacklogItem(BacklogItem item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        ProductBacklog.AddItem(item);
    }
}
