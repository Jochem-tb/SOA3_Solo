namespace AvansDevOps.Domain.Interfaces;

/// <summary>
/// Represents a source control system interface.
/// Part of the Adapter Pattern - bridges external VCS APIs to the core domain.
/// Implementations include GitHub, Atlassian, and other VCS adapters.
/// </summary>
public interface ISourceControl
{
    /// <summary>
    /// Performs a source control operation.
    /// The specific implementation depends on the concrete source control system.
    /// </summary>
    void SomeMethod();
}
