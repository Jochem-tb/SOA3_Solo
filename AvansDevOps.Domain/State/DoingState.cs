using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Domain.State;

/// <summary>
/// Pattern: State Pattern applied here
/// Represents the Doing state of a backlog item.
/// Item is in progress. Can transition to ReadyForTesting or back to Todo.
/// </summary>
public class DoingState : IItemState
{
    private readonly BacklogItem _context;

    public DoingState(BacklogItem context)
    {
        _context = context;
    }

    public void MoveToTodo()
    {
        _context.State = new TodoState(_context);
        _context.NotifyObservers("Item moved back to Todo (rework)");
    }

    public void MoveToDoing()
    {
        // Already in Doing state - no change
    }

    public void MoveToReadyForTesting()
    {
        _context.State = new ReadyForTestingState(_context);
        _context.NotifyObservers("Item moved to ReadyForTesting");
    }

    public void MoveToTesting()
    {
        throw new InvalidOperationException(
            "Cannot move directly from Doing to Testing. Must transition through ReadyForTesting state.");
    }

    public void MoveToTested()
    {
        throw new InvalidOperationException(
            "Cannot move directly from Doing to Tested. Must go through ReadyForTesting, Testing states.");
    }

    public void MoveToDone()
    {
        throw new InvalidOperationException(
            "Cannot move directly from Doing to Done. Must progress through ReadyForTesting, Testing, and Tested states.");
    }

    public Discussion CreateDiscussion()
    {
        return new Discussion();
    }

    public void SendMessage(Discussion discussion, Comment message)
    {
        if (discussion == null)
            throw new ArgumentNullException(nameof(discussion));
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        discussion.Messages.Add(message);
    }
}
