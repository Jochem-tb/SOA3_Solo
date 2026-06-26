using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.State;

namespace AvansDevOps.Tests;

public class ActiveStateTests
{
    [Fact]
    public void AddBacklogItem_WhenCalled_ThrowsInvalidOperationException()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var state = new Domain.State.ActiveState(sprint);
        var item = new BacklogItem { Title = "Story" };

        // Act
        void act() => state.AddBacklogItem(item);

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void EditDetails_WhenCalled_ThrowsInvalidOperationException()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var state = new Domain.State.ActiveState(sprint);

        // Act
        void act() => state.EditDetails("Sprint", new DateTime(2026, 1, 1), new DateTime(2026, 1, 14));

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void Finish_WhenCalled_ChangesSprintStateToFinished()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var state = new Domain.State.ActiveState(sprint);

        // Act
        state.Finish();

        // Assert
        Assert.IsType<FinishedState>(sprint.State);
    }
}
