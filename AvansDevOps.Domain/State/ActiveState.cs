using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Domain.State;

/// <summary>
/// Pattern: State Pattern applied here
/// Represents the Active state of a sprint.
/// In this state, backlog items cannot be added. Only items already in the sprint can be worked on.
/// </summary>
public class ActiveState : ISprintState
{
    private readonly Sprint _context;

    public ActiveState(Sprint context)
    {
        _context = context;
    }

    /// <summary>
    /// Adding backlog items is not allowed in the Active state.
    /// </summary>
    public void AddBacklogItem(BacklogItem item)
    {
        throw new InvalidOperationException(
            "Cannot add backlog items to an active sprint. Only items added in Created state can be worked on.");
    }

    /// <summary>
    /// Editing sprint details is not allowed in the Active state.
    /// </summary>
    public void EditDetails(string name, DateTime startDate, DateTime endDate)
    {
        throw new InvalidOperationException(
            "Cannot edit sprint details while the sprint is active.");
    }

    /// <summary>
    /// Transitions the sprint to the Finished state.
    /// </summary>
    public void Finish()
    {
        _context.State = new FinishedState(_context);
    }
}
