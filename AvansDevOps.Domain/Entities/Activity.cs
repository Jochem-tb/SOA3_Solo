using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Domain.Entities;

/// <summary>
/// Represents an activity (a concrete unit of work).
/// Part of the Composite Pattern - acts as a leaf node with no children.
/// Activities are the smallest unit of work that can be assigned to developers.
/// </summary>
public class Activity : IWorkItem
{
    /// <summary>
    /// Unique identifier for the activity.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The description of the activity.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The developer assigned to this activity.
    /// </summary>
    public User? AssignedDeveloper { get; set; }

    /// <summary>
    /// Indicates whether this activity is marked as done.
    /// </summary>
    public bool DoneStatus { get; set; } = false;

    /// <summary>
    /// Assigns a developer to this activity.
    /// </summary>
    /// <param name="developer">The developer to assign. Can be null to unassign.</param>
    public void AssignDeveloper(User? developer)
    {
        AssignedDeveloper = developer;
    }

    /// <summary>
    /// Checks if this activity is done.
    /// </summary>
    /// <returns>The DoneStatus value.</returns>
    public bool IsDone()
    {
        return DoneStatus;
    }

    /// <summary>
    /// Marks this activity as done.
    /// </summary>
    public void MarkAsDone()
    {
        DoneStatus = true;
    }

    /// <summary>
    /// Marks this activity as not done.
    /// </summary>
    public void MarkAsNotDone()
    {
        DoneStatus = false;
    }
}
