using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;
using AvansDevOps.Domain.State;
using Moq;

namespace AvansDevOps.Tests;

public class TestedStateTests
{
    [Fact]
    public void MoveToTodo_WhenCalled_ThrowsInvalidOperationException()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.TestedState(item);

        // Act
        void act() => item.State.MoveToTodo();

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void MoveToDoing_WhenCalled_ChangesStateAndNotifiesObserver()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.TestedState(item);
        var observerMock = new Mock<IObserver>();
        observerMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        item.Attach(observerMock.Object);

        // Act
        item.State.MoveToDoing();

        // Assert
        Assert.IsType<DoingState>(item.State);
        observerMock.Verify(o => o.Update("Item moved back to Doing (additional rework)", null), Times.Once);
    }

    [Fact]
    public void MoveToReadyForTesting_WhenCalled_ThrowsInvalidOperationException()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.TestedState(item);

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
        item.State = new Domain.State.TestedState(item);

        // Act
        void act() => item.State.MoveToTesting();

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void MoveToTested_WhenAlreadyTested_KeepsState()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.TestedState(item);

        // Act
        item.State.MoveToTested();

        // Assert
        Assert.IsType<Domain.State.TestedState>(item.State);
    }

    [Fact]
    public void MoveToDone_WhenCalled_ChangesStateToDone()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.TestedState(item);
        var observerMock = new Mock<IObserver>();
        observerMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        item.Attach(observerMock.Object);

        // Act
        item.State.MoveToDone();

        // Assert
        Assert.IsType<DoneState>(item.State);
        observerMock.Verify(o => o.Update("Item moved to Done", null), Times.Once);
    }

    [Fact]
    public void CreateDiscussion_WhenCalled_ReturnsEmptyDiscussion()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.TestedState(item);

        // Act
        var discussion = item.State.CreateDiscussion();

        // Assert
        Assert.NotNull(discussion);
        Assert.Empty(discussion.Messages);
    }

    [Fact]
    public void SendMessage_WhenDiscussionIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.TestedState(item);
        var message = new Comment { Message = "Hello" };

        // Act
        void act() => item.State.SendMessage(null!, message);

        // Assert
        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void SendMessage_WhenMessageIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.TestedState(item);
        var discussion = new Discussion();

        // Act
        void act() => item.State.SendMessage(discussion, null!);

        // Assert
        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void SendMessage_WhenInputsValid_AddsMessageToDiscussion()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.TestedState(item);
        var discussion = new Discussion();
        var message = new Comment { Message = "Hello" };

        // Act
        item.State.SendMessage(discussion, message);

        // Assert
        Assert.Single(discussion.Messages);
        Assert.Same(message, discussion.Messages[0]);
    }
}
