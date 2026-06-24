using AvansDevOps.Domain.Entities;

namespace AvansDevOps.Domain.Interfaces;

/// <summary>
/// Represents the state of a sprint.
/// Part of the State Pattern for managing sprint workflow and operations.
/// </summary>
public interface ISprintState
{
    /// <summary>
    /// Adds a backlog item to the sprint.
    /// </summary>
    /// <param name="item">The backlog item to add.</param>
    void AddBacklogItem(BacklogItem item);

    /// <summary>
    /// Edits the sprint details.
    /// </summary>
    /// <param name="name">The new name of the sprint.</param>
    /// <param name="startDate">The new start date of the sprint.</param>
    /// <param name="endDate">The new end date of the sprint.</param>
    void EditDetails(string name, DateTime startDate, DateTime endDate);
}
