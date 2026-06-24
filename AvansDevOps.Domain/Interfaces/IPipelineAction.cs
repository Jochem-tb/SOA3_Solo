namespace AvansDevOps.Domain.Interfaces;

/// <summary>
/// Represents an action in a development pipeline.
/// Part of the Strategy Pattern - defines the contract for different pipeline steps.
/// Implementations include Analyse, Build, Test, Source, Package, and Deploy actions.
/// </summary>
public interface IPipelineAction
{
    /// <summary>
    /// Executes the pipeline action.
    /// </summary>
    /// <returns>True if the action succeeded, false otherwise.</returns>
    bool Execute();

    /// <summary>
    /// Rolls back the changes made by the pipeline action in case of failure.
    /// </summary>
    /// <returns>True if the rollback succeeded, false otherwise.</returns>
    bool Rollback();
}
