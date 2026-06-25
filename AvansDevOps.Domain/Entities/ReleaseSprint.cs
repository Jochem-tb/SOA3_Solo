namespace AvansDevOps.Domain.Entities;

/// <summary>
/// Represents a Release Sprint.
/// Used for final testing and deployment of features to production.
/// May have an associated DevelopmentPipeline for automated deployment.
/// </summary>
public class ReleaseSprint : Sprint
{
    /// <summary>
    /// The development pipeline used for deployment in this release sprint.
    /// </summary>
    public DevelopmentPipeline? DeploymentPipeline { get; set; }

    /// <summary>
    /// Finishes the release sprint.
    /// </summary>
    public override void FinishSprint()
    {
        // Release sprint specific finish logic
        NotifyObservers("ReleaseSprint finished");
    }

    /// <summary>
    /// Finalizes the closing of the release sprint.
    /// Performs release-specific closing operations (e.g., trigger deployment pipeline).
    /// </summary>
    public override void FinalizeClosingOfSprint()
    {
        // Release-specific finalization - could trigger deployment
        if (DeploymentPipeline != null)
        {
            NotifyObservers("ReleaseSprint finalized - deployment pipeline ready");
        }
        else
        {
            NotifyObservers("ReleaseSprint finalized");
        }
    }
}
