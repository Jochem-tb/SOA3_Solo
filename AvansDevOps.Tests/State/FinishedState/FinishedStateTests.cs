using AvansDevOps.Domain.Entities;

namespace AvansDevOps.Tests;

public class FinishedStateTests
{
    [Fact]
    public void AddBacklogItem_WhenCalled_ThrowsInvalidOperationException()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var state = new Domain.State.FinishedState(sprint);
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
        var state = new Domain.State.FinishedState(sprint);

        // Act
        void Act() => state.EditDetails("Sprint", DateTime.UtcNow, DateTime.UtcNow.AddDays(14));

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }
}
