using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Enums;

namespace AvansDevOps.Tests;

public class SprintFactoryTests
{
    [Fact]
    public void CreateSprint_WhenTypeIsReview_ReturnsReviewSprintWithConfiguredProperties()
    {
        // Arrange
        var factory = new Domain.Entities.SprintFactory();
        var expectedName = "Review Sprint";
        var expectedStart = new DateTime(2026, 1, 1);
        var expectedEnd = new DateTime(2026, 1, 14);

        // Act
        var sprint = factory.CreateSprint(SprintType.Review, expectedName, expectedStart, expectedEnd);

        // Assert
        Assert.IsType<ReviewSprint>(sprint);
        Assert.Equal(expectedName, sprint.Name);
        Assert.Equal(expectedStart, sprint.StartDate);
        Assert.Equal(expectedEnd, sprint.EndDate);
    }

    [Fact]
    public void CreateSprint_WhenTypeIsRelease_ReturnsReleaseSprintWithConfiguredProperties()
    {
        // Arrange
        var factory = new Domain.Entities.SprintFactory();
        var expectedName = "Release Sprint";
        var expectedStart = new DateTime(2026, 2, 1);
        var expectedEnd = new DateTime(2026, 2, 14);

        // Act
        var sprint = factory.CreateSprint(SprintType.Release, expectedName, expectedStart, expectedEnd);

        // Assert
        Assert.IsType<ReleaseSprint>(sprint);
        Assert.Equal(expectedName, sprint.Name);
        Assert.Equal(expectedStart, sprint.StartDate);
        Assert.Equal(expectedEnd, sprint.EndDate);
    }

    [Fact]
    public void CreateSprint_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        var factory = new Domain.Entities.SprintFactory();

        // Act
        void Act() => factory.CreateSprint(SprintType.Release, "", DateTime.UtcNow, DateTime.UtcNow.AddDays(1));

        // Assert
        Assert.Throws<ArgumentException>(Act);
    }

    [Fact]
    public void CreateSprint_WhenEndDateIsBeforeStartDate_ThrowsArgumentException()
    {
        // Arrange
        var factory = new Domain.Entities.SprintFactory();
        var startDate = new DateTime(2026, 3, 10);
        var endDate = new DateTime(2026, 3, 9);

        // Act
        void Act() => factory.CreateSprint(SprintType.Release, "Sprint", startDate, endDate);

        // Assert
        Assert.Throws<ArgumentException>(Act);
    }

    [Fact]
    public void CreateSprint_WhenTypeIsUnsupported_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var factory = new Domain.Entities.SprintFactory();
        var unsupportedType = (SprintType)999;

        // Act
        void Act() => factory.CreateSprint(unsupportedType, "Sprint", new DateTime(2026, 4, 1), new DateTime(2026, 4, 14));

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(Act);
    }
}
