using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.State;

namespace AvansDevOps.Tests;

public class BacklogItemStateTests
{
    [Fact]
    public void MoveToDoing_WhenStateIsTodo_ChangesStateToDoing()
    {
        // Arrange
        var item = new Domain.Entities.BacklogItem();
        item.State = new TodoState(item);

        // Act
        item.State.MoveToDoing();

        // Assert
        Assert.IsType<DoingState>(item.State);
    }

    [Fact]
    public void MoveToTesting_WhenStateIsTodo_ThrowsInvalidOperationException()
    {
        // Arrange
        var item = new Domain.Entities.BacklogItem();
        item.State = new TodoState(item);

        // Act
        void Act() => item.State.MoveToTesting();

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }

    [Fact]
    public void MoveToReadyForTesting_WhenStateIsDoing_ChangesStateToReadyForTesting()
    {
        // Arrange
        var item = new Domain.Entities.BacklogItem();
        item.State = new DoingState(item);

        // Act
        item.State.MoveToReadyForTesting();

        // Assert
        Assert.IsType<ReadyForTestingState>(item.State);
    }

    [Fact]
    public void MoveToTesting_WhenStateIsDoing_ThrowsInvalidOperationException()
    {
        // Arrange
        var item = new Domain.Entities.BacklogItem();
        item.State = new DoingState(item);

        // Act
        void Act() => item.State.MoveToTesting();

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }

    [Fact]
    public void MoveToTesting_WhenStateIsReadyForTesting_ChangesStateToTesting()
    {
        // Arrange
        var item = new Domain.Entities.BacklogItem();
        item.State = new ReadyForTestingState(item);

        // Act
        item.State.MoveToTesting();

        // Assert
        Assert.IsType<TestingState>(item.State);
    }

    [Fact]
    public void MoveToTested_WhenStateIsTesting_ChangesStateToTested()
    {
        // Arrange
        var item = new Domain.Entities.BacklogItem();
        item.State = new TestingState(item);

        // Act
        item.State.MoveToTested();

        // Assert
        Assert.IsType<TestedState>(item.State);
    }

    [Fact]
    public void MoveToDone_WhenStateIsTested_ChangesStateToDone()
    {
        // Arrange
        var item = new Domain.Entities.BacklogItem();
        item.State = new TestedState(item);

        // Act
        item.State.MoveToDone();

        // Assert
        Assert.IsType<DoneState>(item.State);
    }

    [Fact]
    public void MoveToDoing_WhenStateIsDone_ThrowsInvalidOperationException()
    {
        // Arrange
        var item = new Domain.Entities.BacklogItem();
        item.State = new DoneState(item);

        // Act
        void Act() => item.State.MoveToDoing();

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }

    [Fact]
    public void MoveToDoing_WhenStateIsTesting_ChangesStateToDoingForFailedTestPath()
    {
        // Arrange
        var item = new Domain.Entities.BacklogItem();
        item.State = new TestingState(item);

        // Act
        item.State.MoveToDoing();

        // Assert
        Assert.IsType<DoingState>(item.State);
    }

    [Fact]
    public void MoveToDoing_WhenStateIsTested_ChangesStateToDoingForDoDFailurePath()
    {
        // Arrange
        var item = new Domain.Entities.BacklogItem();
        item.State = new TestedState(item);

        // Act
        item.State.MoveToDoing();

        // Assert
        Assert.IsType<DoingState>(item.State);
    }

    [Fact]
    public void SendMessage_WhenStateIsDoing_AddsMessageToDiscussion()
    {
        // Arrange
        var item = new Domain.Entities.BacklogItem();
        item.State = new DoingState(item);
        var discussion = item.State.CreateDiscussion();
        var comment = new Comment { Message = "Need review", Time = DateTime.UtcNow };

        // Act
        item.State.SendMessage(discussion, comment);

        // Assert
        Assert.Single(discussion.Messages);
    }

    [Fact]
    public void CreateDiscussion_WhenStateIsDone_ThrowsInvalidOperationException()
    {
        // Arrange
        var item = new Domain.Entities.BacklogItem();
        item.State = new DoneState(item);

        // Act
        void Act() => item.State.CreateDiscussion();

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }

    [Fact]
    public void SendMessage_WhenStateIsDone_ThrowsInvalidOperationException()
    {
        // Arrange
        var item = new Domain.Entities.BacklogItem();
        item.State = new DoneState(item);
        var discussion = new Discussion();
        var comment = new Comment { Message = "Late comment", Time = DateTime.UtcNow };

        // Act
        void Act() => item.State.SendMessage(discussion, comment);

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }
}
