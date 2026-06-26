using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.State;
using DomainActivity = AvansDevOps.Domain.Entities.Activity;
using DomainBacklogItem = AvansDevOps.Domain.Entities.BacklogItem;

namespace AvansDevOps.Tests;

public class SprintStateTests
{
    [Fact]
    public void AddBacklogItem_WhenStateIsCreated_AddsItemToSprintBacklog()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var state = new CreatedState(sprint);
        var item = CreateDoneBacklogItem("Authentication");

        // Act
        state.AddBacklogItem(item);

        // Assert
        Assert.Single(sprint.Backlog.Items);
    }

    [Fact]
    public void EditDetails_WhenStateIsCreatedAndInputIsValid_UpdatesSprintDetails()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var state = new CreatedState(sprint);
        var expectedName = "Sprint A";
        var expectedStart = new DateTime(2026, 4, 1);
        var expectedEnd = new DateTime(2026, 4, 14);

        // Act
        state.EditDetails(expectedName, expectedStart, expectedEnd);

        // Assert
        Assert.Equal(expectedName, sprint.Name);
        Assert.Equal(expectedStart, sprint.StartDate);
        Assert.Equal(expectedEnd, sprint.EndDate);
    }

    [Fact]
    public void EditDetails_WhenEndDateBeforeStartDate_ThrowsArgumentException()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var state = new CreatedState(sprint);

        // Act
        void Act() => state.EditDetails("Sprint A", new DateTime(2026, 4, 14), new DateTime(2026, 4, 1));

        // Assert
        Assert.Throws<ArgumentException>(Act);
    }

    [Fact]
    public void AddBacklogItem_WhenStateIsActive_ThrowsInvalidOperationException()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var state = new ActiveState(sprint);

        // Act
        void Act() => state.AddBacklogItem(new DomainBacklogItem());

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }

    [Fact]
    public void EditDetails_WhenStateIsActive_ThrowsInvalidOperationException()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var state = new ActiveState(sprint);

        // Act
        void Act() => state.EditDetails("Sprint", DateTime.UtcNow, DateTime.UtcNow.AddDays(14));

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }

    [Fact]
    public void Finish_WhenStateIsActive_ChangesStateToFinished()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var state = new ActiveState(sprint);

        // Act
        state.Finish();

        // Assert
        Assert.IsType<FinishedState>(sprint.State);
    }

    [Fact]
    public void AddBacklogItem_WhenStateIsFinished_ThrowsInvalidOperationException()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var state = new FinishedState(sprint);

        // Act
        void Act() => state.AddBacklogItem(new DomainBacklogItem());

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }

    [Fact]
    public void CheckBacklogIsDone_WhenBacklogIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var sprint = new ReviewSprint();

        // Act
        void Act() => sprint.CheckBacklogIsDone(null!);

        // Assert
        Assert.Throws<ArgumentNullException>(Act);
    }

    [Fact]
    public void CheckBacklogIsDone_WhenBacklogContainsUndoneItems_ThrowsInvalidOperationException()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var item = new DomainBacklogItem { Title = "Auth" };
        item.Add(new DomainActivity { Description = "Create endpoint" });
        sprint.Backlog.AddItem(item);

        // Act
        void Act() => sprint.CheckBacklogIsDone(sprint.Backlog);

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }

    [Fact]
    public void CheckBacklogIsDone_WhenAllItemsAreDone_DoesNotThrow()
    {
        // Arrange
        var sprint = new ReviewSprint();
        sprint.Backlog.AddItem(CreateDoneBacklogItem("Auth"));

        // Act
        var exception = Record.Exception(() => sprint.CheckBacklogIsDone(sprint.Backlog));

        // Assert
        Assert.Null(exception);
    }

    private static DomainBacklogItem CreateDoneBacklogItem(string title)
    {
        var item = new DomainBacklogItem { Title = title };
        var activity = new DomainActivity { Description = "Done activity" };
        activity.MarkAsDone();
        item.Add(activity);
        return item;
    }
}
