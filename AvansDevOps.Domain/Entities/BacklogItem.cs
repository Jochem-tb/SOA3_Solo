using AvansDevOps.Domain.Interfaces;
using AvansDevOps.Domain.State;

namespace AvansDevOps.Domain.Entities;

/// <summary>
/// Represents a backlog item (user story, feature, or epic) in the system.
/// Part of the Composite Pattern - acts as an internal node that can contain child work items.
/// Implements the Observer Pattern to notify observers of state changes.
/// Implements the State Pattern to manage workflow transitions.
/// </summary>
/// <remarks>
/// A BacklogItem can be decomposed into smaller Activities (leaf nodes) or other BacklogItems.
/// The composite structure allows hierarchical work decomposition.
/// </remarks>
public class BacklogItem : IWorkItem, IObservable
{
    /// <summary>
    /// Unique identifier for the backlog item.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The title of the backlog item.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The description of the backlog item.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The developer assigned to this backlog item.
    /// </summary>
    public User? AssignedDeveloper { get; set; }

    /// <summary>
    /// Child work items (Activities or other BacklogItems).
    /// Part of the Composite Pattern.
    /// </summary>
    public List<IWorkItem> Children { get; set; } = [];

    /// <summary>
    /// The current state of this backlog item (State Pattern).
    /// Manages workflow transitions and business rules.
    /// </summary>
    public IItemState? State { get; set; }

    /// <summary>
    /// Collection of observers that are notified of state changes (Observer Pattern).
    /// </summary>
    private readonly List<IObserver> _observers = [];

    /// <summary>
    /// Constructor - initializes backlog item with TodoState.
    /// </summary>
    public BacklogItem()
    {
        State = new TodoState(this);
    }

    /// <summary>
    /// Adds a child work item to this backlog item.
    /// </summary>
    /// <param name="item">The child work item to add.</param>
    public void Add(IWorkItem item)
    {
        if (item != null && !Children.Contains(item))
        {
            Children.Add(item);
        }
    }

    /// <summary>
    /// Removes a child work item from this backlog item.
    /// </summary>
    /// <param name="item">The child work item to remove.</param>
    public void Remove(IWorkItem item)
    {
        Children.Remove(item);
    }

    /// <summary>
    /// Assigns a developer to this backlog item.
    /// </summary>
    /// <param name="developer">The developer to assign. Can be null to unassign.</param>
    public void AssignDeveloper(User? developer)
    {
        AssignedDeveloper = developer;
    }

    /// <summary>
    /// Checks if this backlog item and all its children are done.
    /// A backlog item is done when all its children are done.
    /// </summary>
    /// <returns>True if all children are done, false otherwise.</returns>
    public bool IsDone()
    {
        // If there are no children, the backlog item is not done by itself
        if (Children.Count == 0)
        {
            return false;
        }

        // All children must be done for the backlog item to be done
        return Children.All(child => child.IsDone());
    }

    /// <summary>
    /// Attaches an observer to be notified of state changes.
    /// </summary>
    public void Attach(IObserver observer)
    {
        if (observer != null && !_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    /// <summary>
    /// Detaches an observer from receiving state change notifications.
    /// </summary>
    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    /// <summary>
    /// Notifies all attached observers of a state change.
    /// </summary>
    public void NotifyObservers(string context = "State changed")
    {
        foreach (var observer in _observers)
        {
            observer.Update(context, AssignedDeveloper);
        }
    }
}
