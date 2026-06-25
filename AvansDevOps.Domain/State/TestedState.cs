using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Domain.State;

/// <summary>
/// Pattern: State Pattern applied here
/// Represents the Tested state of a backlog item.
/// Item has passed testing. Can transition to Done or back to Doing.
/// </summary>
public class TestedState : IItemState
{
    private readonly BacklogItem _context;

    public TestedState(BacklogItem context)
    {
        _context = context;
    }

    public void MoveToTodo()
    {
        throw new InvalidOperationException(
            "Cannot move back to Todo from Tested. Can only return to Doing.");
    }

    public void MoveToDoing()
    {
        _context.State = new DoingState(_context);
        _context.NotifyObservers("Item moved back to Doing (additional rework)");
    }

    public void MoveToReadyForTesting()
    {
        throw new InvalidOperationException(
            "Cannot move back to ReadyForTesting from Tested. Must return to Doing for rework.");
    }

    public void MoveToTesting()
    {
        throw new InvalidOperationException(
            "Cannot move back to Testing from Tested. Must return to Doing for rework.");
    }

    public void MoveToTested()
    {
        // Already in Tested state - no change
    }

    public void MoveToDone()
    {
        _context.State = new DoneState(_context);
        _context.NotifyObservers("Item moved to Done");
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
