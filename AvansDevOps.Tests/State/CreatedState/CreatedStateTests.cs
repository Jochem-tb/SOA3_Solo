using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.State;

namespace AvansDevOps.Tests;

public class CreatedStateTests
{
    [Fact]
    public void AddBacklogItem_WhenItemIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var state = new Domain.State.CreatedState(sprint);

        // Act
        void act() => state.AddBacklogItem(null!);

        // Assert
        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void AddBacklogItem_WhenItemIsValid_AddsItemToBacklog()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var state = new Domain.State.CreatedState(sprint);
        var item = new BacklogItem { Title = "Story" };

        // Act
        state.AddBacklogItem(item);

        // Assert
        Assert.Single(sprint.Backlog.Items);
    }

    [Fact]
    public void EditDetails_WhenNameIsWhitespace_ThrowsArgumentException()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var state = new Domain.State.CreatedState(sprint);

        // Act
        void act() => state.EditDetails("  ", new DateTime(2026, 1, 1), new DateTime(2026, 1, 2));

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void EditDetails_WhenEndDateNotAfterStartDate_ThrowsArgumentException()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var state = new Domain.State.CreatedState(sprint);

        // Act
        void act() => state.EditDetails("Sprint", new DateTime(2026, 1, 2), new DateTime(2026, 1, 2));

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void EditDetails_WhenInputIsValid_UpdatesSprintFields()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var state = new Domain.State.CreatedState(sprint);
        var expectedName = "Sprint X";
        var expectedStart = new DateTime(2026, 2, 1);
        var expectedEnd = new DateTime(2026, 2, 14);

        // Act
        state.EditDetails(expectedName, expectedStart, expectedEnd);

        // Assert
        Assert.Equal(expectedName, sprint.Name);
        Assert.Equal(expectedStart, sprint.StartDate);
        Assert.Equal(expectedEnd, sprint.EndDate);
    }

    [Fact]
    public void Activate_WhenCalled_ChangesSprintStateToActive()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var state = new Domain.State.CreatedState(sprint);

        // Act
        state.Activate();

        // Assert
        Assert.IsType<ActiveState>(sprint.State);
    }
}
