using AvansDevOps.Domain.Entities;

namespace AvansDevOps.Domain.Interfaces;

/// <summary>
/// Represents an item that can be assigned to a developer and tracked for completion.
/// Part of the Composite Pattern - acts as the root interface for both composite (BacklogItem) and leaf (Activity) nodes.
/// </summary>
public interface IWorkItem
{
    /// <summary>
    /// Assigns a developer to this work item.
    /// </summary>
    /// <param name="developer">The developer to assign. Can be null to unassign.</param>
    void AssignDeveloper(User? developer);

    /// <summary>
    /// Checks if this work item is marked as done.
    /// </summary>
    /// <returns>True if the item is done, false otherwise.</returns>
    bool IsDone();
}
