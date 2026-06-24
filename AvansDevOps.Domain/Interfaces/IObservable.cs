namespace AvansDevOps.Domain.Interfaces;

/// <summary>
/// Represents an object that can be observed by IObserver instances.
/// Part of the Observer Pattern. Context classes (Sprint, BacklogItem) implement this interface.
/// </summary>
public interface IObservable
{
    /// <summary>
    /// Attaches an observer to this observable.
    /// </summary>
    /// <param name="observer">The observer to attach.</param>
    void Attach(IObserver observer);

    /// <summary>
    /// Detaches an observer from this observable.
    /// </summary>
    /// <param name="observer">The observer to detach.</param>
    void Detach(IObserver observer);

    /// <summary>
    /// Notifies all attached observers of a state change.
    /// </summary>
    void NotifyObservers();
}
