using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Domain.State;

/// <summary>
/// Pattern: State Pattern applied here
/// Represents the Todo state of a backlog item.
/// Initial state for new items. Can transition to Doing or be removed.
/// </summary>
public class TodoState : IItemState
{
    private readonly BacklogItem _context;

    public TodoState(BacklogItem context)
    {
        _context = context;
    }

    public void MoveToTodo()
    {
        // Already in Todo state - no change
    }

    public void MoveToDoing()
    {
        _context.State = new DoingState(_context);
        _context.NotifyObservers("Item moved to Doing");
    }

    public void MoveToReadyForTesting()
    {
        throw new InvalidOperationException(
            "Cannot move directly from Todo to ReadyForTesting. Must go through Doing state.");
    }

    public void MoveToTesting()
    {
        throw new InvalidOperationException(
            "Cannot move directly from Todo to Testing. Must go through Doing and ReadyForTesting states.");
    }

    public void MoveToTested()
    {
        throw new InvalidOperationException(
            "Cannot move directly from Todo to Tested. Must progress through all states.");
    }

    public void MoveToDone()
    {
        throw new InvalidOperationException(
            "Cannot move directly from Todo to Done. Must progress through Doing, ReadyForTesting, Testing, and Tested states.");
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
