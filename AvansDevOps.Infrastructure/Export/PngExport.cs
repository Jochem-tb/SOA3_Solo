// Pattern: Strategy Pattern applied here
using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Infrastructure.Export;

/// <summary>
/// Export strategy for PNG sprint reports.
/// </summary>
public class PngExport : IExportStrategy
{
    public void Export()
    {
        // Stub export behavior.
    }
}