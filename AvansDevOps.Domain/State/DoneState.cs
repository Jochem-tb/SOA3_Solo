using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Domain.State;

/// <summary>
/// Pattern: State Pattern applied here
/// Represents the Done state of a backlog item.
/// Final state. No further transitions allowed. No new discussion messages can be added.
/// </summary>
public class DoneState : IItemState
{
    private readonly BacklogItem _context;

    public DoneState(BacklogItem context)
    {
        _context = context;
    }

    public void MoveToTodo()
    {
        throw new InvalidOperationException(
            "Cannot move back to Todo from Done. Item is complete and archived.");
    }

    public void MoveToDoing()
    {
        throw new InvalidOperationException(
            "Cannot move back to Doing from Done. Item is complete and archived.");
    }

    public void MoveToReadyForTesting()
    {
        throw new InvalidOperationException(
            "Cannot modify a Done item. Item is complete and archived.");
    }

    public void MoveToTesting()
    {
        throw new InvalidOperationException(
            "Cannot modify a Done item. Item is complete and archived.");
    }

    public void MoveToTested()
    {
        throw new InvalidOperationException(
            "Cannot modify a Done item. Item is complete and archived.");
    }

    public void MoveToDone()
    {
        // Already in Done state - no change
    }

    public Discussion CreateDiscussion()
    {
        throw new InvalidOperationException(
            "Cannot create discussions on Done items. Item is complete and archived.");
    }

    public void SendMessage(Discussion discussion, Comment message)
    {
        throw new InvalidOperationException(
            "Cannot add messages to discussions on Done items. Item is complete and archived.");
    }
}
