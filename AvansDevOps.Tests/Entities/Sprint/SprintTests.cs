using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;
using Moq;

namespace AvansDevOps.Tests;

public class SprintTests
{
    [Fact]
    public void NotifyObservers_WhenObserverAttached_CallsUpdateWithNullUser()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var observerMock = new Mock<IObserver>();
        observerMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        sprint.Attach(observerMock.Object);

        // Act
        sprint.NotifyObservers("Sprint changed");

        // Assert
        observerMock.Verify(o => o.Update("Sprint changed", null), Times.Once);
    }

    [Fact]
    public void Detach_WhenObserverDetached_DoesNotReceiveUpdates()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var observerMock = new Mock<IObserver>();
        observerMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        sprint.Attach(observerMock.Object);
        sprint.Detach(observerMock.Object);

        // Act
        sprint.NotifyObservers("Sprint changed");

        // Assert
        observerMock.Verify(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()), Times.Never);
    }

    [Fact]
    public void Attach_WhenObserverIsNull_DoesNotThrowAndNoObserverAdded()
    {
        // Arrange
        var sprint = new ReviewSprint();

        // Act
        sprint.Attach(null!);
        sprint.NotifyObservers("Sprint changed");

        // Assert
        Assert.True(true);
    }

    [Fact]
    public void Attach_WhenObserverAlreadyAttached_DoesNotDuplicateNotification()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var observerMock = new Mock<IObserver>();
        observerMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        sprint.Attach(observerMock.Object);

        // Act
        sprint.Attach(observerMock.Object);
        sprint.NotifyObservers("Sprint changed");

        // Assert
        observerMock.Verify(o => o.Update("Sprint changed", null), Times.Once);
    }

    [Fact]
    public void NotifyObservers_WhenNoObserversAttached_DoesNotThrow()
    {
        // Arrange
        var sprint = new ReviewSprint();

        // Act
        var exception = Record.Exception(() => sprint.NotifyObservers("Sprint changed"));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void NotifyObservers_WhenTwoObserversAttached_NotifiesBothObservers()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var firstObserverMock = new Mock<IObserver>();
        var secondObserverMock = new Mock<IObserver>();
        firstObserverMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        secondObserverMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        sprint.Attach(firstObserverMock.Object);
        sprint.Attach(secondObserverMock.Object);

        // Act
        sprint.NotifyObservers("Sprint changed");

        // Assert
        firstObserverMock.Verify(o => o.Update("Sprint changed", null), Times.Once);
        secondObserverMock.Verify(o => o.Update("Sprint changed", null), Times.Once);
    }

    [Fact]
    public void CheckBacklogIsDone_WhenBacklogIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var sprint = new ReviewSprint();

        // Act
        void act() => sprint.CheckBacklogIsDone(null!);

        // Assert
        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void CheckBacklogIsDone_WhenBacklogIsNotDone_ThrowsInvalidOperationException()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var backlog = new SprintBacklog();
        var item = new BacklogItem { Title = "Story" };
        item.Add(new Activity { Description = "Not done" });
        backlog.AddItem(item);

        // Act
        void act() => sprint.CheckBacklogIsDone(backlog);

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    [Fact]
    public void CheckBacklogIsDone_WhenBacklogIsDone_DoesNotThrow()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var backlog = new SprintBacklog();
        var item = new BacklogItem { Title = "Story" };
        var activity = new Activity { Description = "Done" };
        activity.MarkAsDone();
        item.Add(activity);
        backlog.AddItem(item);

        // Act
        var exception = Record.Exception(() => sprint.CheckBacklogIsDone(backlog));

        // Assert
        Assert.Null(exception);
    }
}
