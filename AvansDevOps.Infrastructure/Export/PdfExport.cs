// Pattern: Strategy Pattern applied here
using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Infrastructure.Export;

/// <summary>
/// Export strategy for PDF sprint reports.
/// </summary>
public class PdfExport : IExportStrategy
{
    public void Export()
    {
        // Stub export behavior.
    }
}