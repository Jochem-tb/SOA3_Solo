// Pattern: Factory Pattern applied here
using AvansDevOps.Domain.Enums;

namespace AvansDevOps.Domain.Entities;

/// <summary>
/// Creates sprint instances based on SprintType.
/// </summary>
public class SprintFactory
{
    /// <summary>
    /// Creates a sprint of the requested type.
    /// </summary>
    /// <param name="type">The sprint type to create.</param>
    /// <param name="name">The sprint name.</param>
    /// <param name="startDate">Sprint start date.</param>
    /// <param name="endDate">Sprint end date.</param>
    /// <returns>A configured sprint instance.</returns>
    public Sprint CreateSprint(SprintType type, string name, DateTime startDate, DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Sprint name cannot be empty.", nameof(name));
        }

        if (endDate <= startDate)
        {
            throw new ArgumentException("End date must be after start date.");
        }

        Sprint sprint;
        switch (type)
        {
            case SprintType.Review:
                sprint = new ReviewSprint();
                break;
            case SprintType.Release:
                sprint = new ReleaseSprint();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, "Unsupported sprint type.");
        }

        sprint.Name = name;
        sprint.StartDate = startDate;
        sprint.EndDate = endDate;
        return sprint;
    }
}