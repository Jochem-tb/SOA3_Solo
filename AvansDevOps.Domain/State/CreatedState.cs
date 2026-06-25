using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Domain.State;

/// <summary>
/// Pattern: State Pattern applied here
/// Represents the Created state of a sprint.
/// In this state, backlog items can be added to the sprint.
/// </summary>
public class CreatedState : ISprintState
{
    private readonly Sprint _context;

    public CreatedState(Sprint context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a backlog item to the sprint.
    /// Only allowed in the Created state.
    /// </summary>
    public void AddBacklogItem(BacklogItem item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        _context.Backlog.AddItem(item);
    }

    /// <summary>
    /// Edits the sprint details.
    /// Allowed in the Created state.
    /// </summary>
    public void EditDetails(string name, DateTime startDate, DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Sprint name cannot be empty.", nameof(name));

        if (endDate <= startDate)
            throw new ArgumentException("End date must be after start date.");

        _context.Name = name;
        _context.StartDate = startDate;
        _context.EndDate = endDate;
    }

    /// <summary>
    /// Transitions the sprint to the Active state.
    /// </summary>
    public void Activate()
    {
        _context.State = new ActiveState(_context);
    }
}
