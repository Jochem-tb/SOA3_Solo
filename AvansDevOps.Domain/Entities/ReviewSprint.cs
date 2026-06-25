namespace AvansDevOps.Domain.Entities;

/// <summary>
/// Represents a Review Sprint.
/// Used for reviewing completed work from a previous sprint.
/// </summary>
public class ReviewSprint : Sprint
{
    /// <summary>
    /// Finishes the review sprint.
    /// </summary>
    public override void FinishSprint()
    {
        // Review sprint specific finish logic
        NotifyObservers("ReviewSprint finished");
    }

    /// <summary>
    /// Finalizes the closing of the review sprint.
    /// Performs review-specific closing operations (e.g., generate review report).
    /// </summary>
    public override void FinalizeClosingOfSprint()
    {
        // Review-specific finalization
        NotifyObservers("ReviewSprint finalized");
    }
}
