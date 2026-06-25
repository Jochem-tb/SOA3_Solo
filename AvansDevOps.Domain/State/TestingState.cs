using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Domain.State;

/// <summary>
/// Pattern: State Pattern applied here
/// Represents the Testing state of a backlog item.
/// Item is undergoing testing. Can transition to Tested or back to Doing.
/// </summary>
public class TestingState : IItemState
{
    private readonly BacklogItem _context;

    public TestingState(BacklogItem context)
    {
        _context = context;
    }

    public void MoveToTodo()
    {
        throw new InvalidOperationException(
            "Cannot move back to Todo from Testing. Can only return to Doing.");
    }

    public void MoveToDoing()
    {
        _context.State = new DoingState(_context);
        _context.NotifyObservers("Item moved back to Doing (failed testing)");
    }

    public void MoveToReadyForTesting()
    {
        throw new InvalidOperationException(
            "Cannot move back to ReadyForTesting from Testing. Must return to Doing for rework.");
    }

    public void MoveToTesting()
    {
        // Already in Testing state - no change
    }

    public void MoveToTested()
    {
        _context.State = new TestedState(_context);
        _context.NotifyObservers("Item moved to Tested");
    }

    public void MoveToDone()
    {
        throw new InvalidOperationException(
            "Cannot move directly from Testing to Done. Must transition through Tested state.");
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
