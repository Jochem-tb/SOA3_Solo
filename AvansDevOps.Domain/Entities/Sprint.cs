using AvansDevOps.Domain.Interfaces;
using AvansDevOps.Domain.State;

namespace AvansDevOps.Domain.Entities;

/// <summary>
/// Represents an abstract base class for a sprint in the system.
/// A sprint is a time-boxed iteration for implementing items from the product backlog.
/// Implements the Observer Pattern to notify observers of state changes.
/// Implements the State Pattern to manage sprint workflow transitions.
/// Subclasses: ReviewSprint, ReleaseSprint
/// </summary>
public abstract class Sprint : IObservable
{
    /// <summary>
    /// Unique identifier for the sprint.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The name of the sprint.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The start date of the sprint.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// The end date of the sprint.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// The current state of the sprint (State Pattern).
    /// Controls what operations are allowed on the sprint.
    /// </summary>
    public ISprintState? State { get; set; }

    /// <summary>
    /// The backlog items assigned to this sprint.
    /// </summary>
    public SprintBacklog Backlog { get; set; } = new();

    /// <summary>
    /// Collection of observers that are notified of state changes (Observer Pattern).
    /// </summary>
    private readonly List<IObserver> _observers = [];

    /// <summary>
    /// Constructor - initializes sprint with CreatedState.
    /// </summary>
    protected Sprint()
    {
        State = new CreatedState(this);
    }

    /// <summary>
    /// Finishes the sprint and marks it as completed.
    /// </summary>
    public abstract void FinishSprint();

    /// <summary>
    /// Checks if the sprint backlog is done (all items completed).
    /// </summary>
    /// <param name="backlog">The sprint backlog to check.</param>
    /// <returns>True if all items are done, false otherwise.</returns>
    public virtual void CheckBacklogIsDone(SprintBacklog backlog)
    {
        if (backlog == null)
        {
            throw new ArgumentNullException(nameof(backlog));
        }

        if (!backlog.IsDone())
        {
            throw new InvalidOperationException("Cannot finalize sprint: not all backlog items are done.");
        }
    }

    /// <summary>
    /// Finalizes the closing of the sprint.
    /// This is overridden in subclasses to perform sprint-specific closing logic.
    /// </summary>
    public abstract void FinalizeClosingOfSprint();

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
    public void NotifyObservers(string context = "Sprint state changed")
    {
        foreach (var observer in _observers)
        {
            observer.Update(context, null);
        }
    }
}
