using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Domain.State;

/// <summary>
/// Pattern: State Pattern applied here
/// Represents the Finished state of a sprint.
/// In this state, no operations are allowed. The sprint is complete and archived.
/// </summary>
public class FinishedState : ISprintState
{
    private readonly Sprint _context;

    public FinishedState(Sprint context)
    {
        _context = context;
    }

    /// <summary>
    /// Adding backlog items is not allowed in the Finished state.
    /// </summary>
    public void AddBacklogItem(BacklogItem item)
    {
        throw new InvalidOperationException(
            "Cannot modify a finished sprint. The sprint has been archived.");
    }

    /// <summary>
    /// Editing sprint details is not allowed in the Finished state.
    /// </summary>
    public void EditDetails(string name, DateTime startDate, DateTime endDate)
    {
        throw new InvalidOperationException(
            "Cannot modify a finished sprint. The sprint has been archived.");
    }
}
