using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Domain.Entities;

/// <summary>
/// Represents a development pipeline containing a series of pipeline actions.
/// Used in Release Sprints for automated deployment and testing.
/// </summary>
public class DevelopmentPipeline
{
    /// <summary>
    /// Unique identifier for the pipeline.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The ordered list of pipeline actions to execute.
    /// </summary>
    public List<IPipelineAction> Pipeline { get; set; } = [];

    /// <summary>
    /// Adds an action to the pipeline.
    /// </summary>
    public void AddAction(IPipelineAction action)
    {
        if (action != null)
        {
            Pipeline.Add(action);
        }
    }

    /// <summary>
    /// Removes an action from the pipeline by index.
    /// </summary>
    public void RemoveAction(int index)
    {
        if (index >= 0 && index < Pipeline.Count)
        {
            Pipeline.RemoveAt(index);
        }
    }

    /// <summary>
    /// Executes all actions in the pipeline sequentially.
    /// Rolls back on failure.
    /// </summary>
    public bool Execute()
    {
        var executedActions = new List<IPipelineAction>();

        try
        {
            foreach (var action in Pipeline)
            {
                if (!action.Execute())
                {
                    // Rollback executed actions in reverse order
                    for (int i = executedActions.Count - 1; i >= 0; i--)
                    {
                        executedActions[i].Rollback();
                    }
                    return false;
                }
                executedActions.Add(action);
            }
            return true;
        }
        catch
        {
            // Rollback on exception
            for (int i = executedActions.Count - 1; i >= 0; i--)
            {
                executedActions[i].Rollback();
            }
            throw;
        }
    }
}
