using AvansDevOps.Domain.Entities;

namespace AvansDevOps.Domain.Interfaces;

/// <summary>
/// Represents the state of a backlog item in the workflow.
/// Part of the State Pattern for managing item state transitions and discussion threads.
/// Discussion logic lives here to prevent messages being added to completed items.
/// </summary>
public interface IItemState
{
    /// <summary>
    /// Transitions the item to the Todo state.
    /// </summary>
    void MoveToTodo();

    /// <summary>
    /// Transitions the item to the Doing state.
    /// </summary>
    void MoveToDoing();

    /// <summary>
    /// Transitions the item to the ReadyForTesting state.
    /// </summary>
    void MoveToReadyForTesting();

    /// <summary>
    /// Transitions the item to the Testing state.
    /// </summary>
    void MoveToTesting();

    /// <summary>
    /// Transitions the item to the Tested state.
    /// </summary>
    void MoveToTested();

    /// <summary>
    /// Transitions the item to the Done state.
    /// </summary>
    void MoveToDone();

    /// <summary>
    /// Creates a new discussion thread for this item.
    /// </summary>
    /// <returns>The newly created discussion.</returns>
    Discussion CreateDiscussion();

    /// <summary>
    /// Sends a message to a discussion thread.
    /// The state controls whether messages can be added (e.g., not in Done state).
    /// </summary>
    /// <param name="discussion">The discussion to send the message to.</param>
    /// <param name="message">The comment message to add.</param>
    void SendMessage(Discussion discussion, Comment message);
}
