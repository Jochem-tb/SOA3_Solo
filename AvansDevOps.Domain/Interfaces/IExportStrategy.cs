namespace AvansDevOps.Domain.Interfaces;

/// <summary>
/// Represents an export strategy for generating sprint reports.
/// Part of the Strategy Pattern - defines different ways to export sprint data.
/// Implementations include PDF and PNG export strategies.
/// </summary>
public interface IExportStrategy
{
    /// <summary>
    /// Exports the sprint data using the specific export strategy.
    /// </summary>
    /// <returns>The exported data as a byte array or file reference.</returns>
    void Export();
}
