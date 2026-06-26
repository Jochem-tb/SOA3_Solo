using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.State;

namespace AvansDevOps.Tests;

public class DoneStateTests
{
    [Fact]
    public void MoveToTodo_WhenCalled_ThrowsInvalidOperationException()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.DoneState(item);

        // Act
        void act() => item.State.MoveToTodo();

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void MoveToDoing_WhenCalled_ThrowsInvalidOperationException()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.DoneState(item);

        // Act
        void act() => item.State.MoveToDoing();

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void MoveToReadyForTesting_WhenCalled_ThrowsInvalidOperationException()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.DoneState(item);

        // Act
        void act() => item.State.MoveToReadyForTesting();

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void MoveToTesting_WhenCalled_ThrowsInvalidOperationException()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.DoneState(item);

        // Act
        void act() => item.State.MoveToTesting();

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void MoveToTested_WhenCalled_ThrowsInvalidOperationException()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.DoneState(item);

        // Act
        void act() => item.State.MoveToTested();

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void MoveToDone_WhenAlreadyDone_KeepsStateAsDone()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.DoneState(item);

        // Act
        item.State.MoveToDone();

        // Assert
        Assert.IsType<DoneState>(item.State);
    }

    [Fact]
    public void CreateDiscussion_WhenCalled_ThrowsInvalidOperationException()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.DoneState(item);

        // Act
        void Act() => item.State.CreateDiscussion();

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }

    [Fact]
    public void SendMessage_WhenCalled_ThrowsInvalidOperationException()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.DoneState(item);
        var discussion = new Discussion();
        var message = new Comment { Message = "Should fail" };

        // Act
        void act() => item.State.SendMessage(discussion, message);

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }
}
