using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;
using AvansDevOps.Domain.State;
using Moq;

namespace AvansDevOps.Tests;

public class ReadyForTestingStateTests
{
    [Fact]
    public void MoveToTodo_WhenCalled_ThrowsInvalidOperationException()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.ReadyForTestingState(item);

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
        item.State = new Domain.State.ReadyForTestingState(item);
        var observerMock = new Mock<IObserver>();
        observerMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        item.Attach(observerMock.Object);

        // Act
        item.State.MoveToDoing();

        // Assert
        Assert.IsType<DoingState>(item.State);
        observerMock.Verify(o => o.Update("Item moved back to Doing (rework)", null), Times.Once);
    }

    [Fact]
    public void MoveToReadyForTesting_WhenAlreadyReadyForTesting_KeepsState()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.ReadyForTestingState(item);

        // Act
        item.State.MoveToReadyForTesting();

        // Assert
        Assert.IsType<Domain.State.ReadyForTestingState>(item.State);
    }

    [Fact]
    public void MoveToTesting_WhenCalled_ChangesStateAndNotifiesObserver()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.ReadyForTestingState(item);
        var observerMock = new Mock<IObserver>();
        observerMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        item.Attach(observerMock.Object);

        // Act
        item.State.MoveToTesting();

        // Assert
        Assert.IsType<TestingState>(item.State);
        observerMock.Verify(o => o.Update("Item moved to Testing", null), Times.Once);
    }

    [Fact]
    public void MoveToTested_WhenCalled_ThrowsInvalidOperationException()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.ReadyForTestingState(item);

        // Act
        void act() => item.State.MoveToTested();

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void MoveToDone_WhenCalled_ThrowsInvalidOperationException()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.ReadyForTestingState(item);

        // Act
        void act() => item.State.MoveToDone();

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void CreateDiscussion_WhenCalled_ReturnsEmptyDiscussion()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new Domain.State.ReadyForTestingState(item);

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
        item.State = new Domain.State.ReadyForTestingState(item);
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
        item.State = new Domain.State.ReadyForTestingState(item);
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
        item.State = new Domain.State.ReadyForTestingState(item);
        var discussion = new Discussion();
        var message = new Comment { Message = "Hello" };

        // Act
        item.State.SendMessage(discussion, message);

        // Assert
        Assert.Single(discussion.Messages);
        Assert.Same(message, discussion.Messages[0]);
    }
}
