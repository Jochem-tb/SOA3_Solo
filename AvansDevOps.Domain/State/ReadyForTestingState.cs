using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Domain.State;

/// <summary>
/// Pattern: State Pattern applied here
/// Represents the ReadyForTesting state of a backlog item.
/// Item is ready for QA/testing. Can transition to Testing or back to Doing.
/// </summary>
public class ReadyForTestingState : IItemState
{
    private readonly BacklogItem _context;

    public ReadyForTestingState(BacklogItem context)
    {
        _context = context;
    }

    public void MoveToTodo()
    {
        throw new InvalidOperationException(
            "Cannot move back to Todo from ReadyForTesting. Can only return to Doing.");
    }

    public void MoveToDoing()
    {
        _context.State = new DoingState(_context);
        _context.NotifyObservers("Item moved back to Doing (rework)");
    }

    public void MoveToReadyForTesting()
    {
        // Already in ReadyForTesting state - no change
    }

    public void MoveToTesting()
    {
        _context.State = new TestingState(_context);
        _context.NotifyObservers("Item moved to Testing");
    }

    public void MoveToTested()
    {
        throw new InvalidOperationException(
            "Cannot move directly from ReadyForTesting to Tested. Must transition through Testing state.");
    }

    public void MoveToDone()
    {
        throw new InvalidOperationException(
            "Cannot move directly from ReadyForTesting to Done. Must progress through Testing and Tested states.");
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
