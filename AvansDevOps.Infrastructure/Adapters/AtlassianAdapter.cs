// Pattern: Adapter Pattern applied here
using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Infrastructure.Adapters;

/// <summary>
/// Adapter stub for Atlassian source control integration.
/// </summary>
public class AtlassianAdapter : ISourceControl
{
    public void SomeMethod()
    {
        // Stub for source-control integration behavior.
    }
}