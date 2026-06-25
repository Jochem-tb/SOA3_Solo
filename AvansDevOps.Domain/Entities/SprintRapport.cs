// Pattern: Strategy Pattern applied here
using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Domain.Entities;

/// <summary>
/// Represents sprint report configuration using an export strategy.
/// </summary>
public class SprintRapport
{
    /// <summary>
    /// The export strategy used to produce the sprint report output.
    /// </summary>
    public IExportStrategy ExportStrategy { get; set; }

    public SprintRapport(IExportStrategy exportStrategy)
    {
        ExportStrategy = exportStrategy ?? throw new ArgumentNullException(nameof(exportStrategy));
    }
}