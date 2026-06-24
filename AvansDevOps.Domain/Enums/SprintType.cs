namespace AvansDevOps.Domain.Enums;

/// <summary>
/// Defines the types of sprints that can be created in the system.
/// </summary>
public enum SprintType
{
    /// <summary>
    /// Sprint dedicated to reviewing completed work.
    /// </summary>
    Review,

    /// <summary>
    /// Sprint dedicated to releasing features to production.
    /// </summary>
    Release
}
