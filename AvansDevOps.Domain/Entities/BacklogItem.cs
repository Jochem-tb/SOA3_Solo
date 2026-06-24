using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Domain.Entities;

/// <summary>
/// Represents a backlog item in a sprint or product backlog.
/// Part of the Composite Pattern - acts as an internal node.
/// Can contain child work items and has a workflow state.
/// </summary>
public class BacklogItem : IWorkItem
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // To be fully implemented with state pattern
    public IItemState? State { get; set; }
    
    // Composite pattern children
    public List<IWorkItem> Children { get; set; } = [];

    public void AssignDeveloper(User? developer)
    {
        // To be implemented
    }

    public bool IsDone()
    {
        // To be implemented
        return false;
    }
}
