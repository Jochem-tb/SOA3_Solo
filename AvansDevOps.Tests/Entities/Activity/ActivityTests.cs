using AvansDevOps.Domain.Entities;

namespace AvansDevOps.Tests;

public class ActivityTests
{
    [Fact]
    public void MarkAsDone_WhenCalled_SetsDoneStatusToTrue()
    {
        // Arrange
        var activity = new Activity { Description = "Implement API" };

        // Act
        activity.MarkAsDone();

        // Assert
        Assert.True(activity.DoneStatus);
    }

    [Fact]
    public void MarkAsNotDone_WhenCalledAfterDone_SetsDoneStatusToFalse()
    {
        // Arrange
        var activity = new Activity { Description = "Implement API" };
        activity.MarkAsDone();

        // Act
        activity.MarkAsNotDone();

        // Assert
        Assert.False(activity.DoneStatus);
    }
}
