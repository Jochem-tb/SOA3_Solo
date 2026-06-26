using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;
using AvansDevOps.Domain.State;
using Moq;

namespace AvansDevOps.Tests;

public class BacklogItemTests
{
    [Fact]
    public void Add_WhenChildIsNull_DoesNotAddChild()
    {
        // Arrange
        var backlogItem = new Domain.Entities.BacklogItem();

        // Act
        backlogItem.Add(null!);

        // Assert
        Assert.Empty(backlogItem.Children);
    }

    [Fact]
    public void Add_WhenChildIsUnique_AddsChildOnce()
    {
        // Arrange
        var backlogItem = new Domain.Entities.BacklogItem();
        var child = new Activity { Description = "Create endpoint" };

        // Act
        backlogItem.Add(child);

        // Assert
        Assert.Single(backlogItem.Children);
    }

    [Fact]
    public void Add_WhenChildAlreadyExists_DoesNotDuplicate()
    {
        // Arrange
        var backlogItem = new Domain.Entities.BacklogItem();
        var child = new Activity { Description = "Create endpoint" };
        backlogItem.Add(child);

        // Act
        backlogItem.Add(child);

        // Assert
        Assert.Single(backlogItem.Children);
    }

    [Fact]
    public void Remove_WhenChildExists_RemovesChild()
    {
        // Arrange
        var backlogItem = new Domain.Entities.BacklogItem();
        var child = new Activity { Description = "Create endpoint" };
        backlogItem.Add(child);

        // Act
        backlogItem.Remove(child);

        // Assert
        Assert.Empty(backlogItem.Children);
    }

    [Fact]
    public void IsDone_WhenNoChildren_ReturnsFalse()
    {
        // Arrange
        var backlogItem = new Domain.Entities.BacklogItem();

        // Act
        var result = backlogItem.IsDone();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsDone_WhenAnyChildNotDone_ReturnsFalse()
    {
        // Arrange
        var backlogItem = new Domain.Entities.BacklogItem();
        var doneActivity = new Activity { Description = "Done activity" };
        var todoActivity = new Activity { Description = "Todo activity" };
        doneActivity.MarkAsDone();
        backlogItem.Add(doneActivity);
        backlogItem.Add(todoActivity);

        // Act
        var result = backlogItem.IsDone();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsDone_WhenAllChildrenDone_ReturnsTrue()
    {
        // Arrange
        var backlogItem = new Domain.Entities.BacklogItem();
        var activity1 = new Activity { Description = "Activity 1" };
        var activity2 = new Activity { Description = "Activity 2" };
        activity1.MarkAsDone();
        activity2.MarkAsDone();
        backlogItem.Add(activity1);
        backlogItem.Add(activity2);

        // Act
        var result = backlogItem.IsDone();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void NotifyObservers_WhenObserverAttached_CallsUpdateWithContextAndDeveloper()
    {
        // Arrange
        var expectedContext = "Item moved to Doing";
        var expectedDeveloper = new User { Name = "Bob" };
        var backlogItem = new Domain.Entities.BacklogItem
        {
            AssignedDeveloper = expectedDeveloper
        };
        var observerMock = new Mock<IObserver>();
        observerMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        backlogItem.Attach(observerMock.Object);
        backlogItem.State = new TodoState(backlogItem);

        // Act
        backlogItem.State.MoveToDoing();

        // Assert
        observerMock.Verify(o => o.Update(expectedContext, expectedDeveloper), Times.Once);
    }

    [Fact]
    public void Detach_WhenObserverDetached_DoesNotReceiveFurtherUpdates()
    {
        // Arrange
        var backlogItem = new Domain.Entities.BacklogItem();
        var observerMock = new Mock<IObserver>();
        observerMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        backlogItem.Attach(observerMock.Object);
        backlogItem.Detach(observerMock.Object);

        // Act
        backlogItem.NotifyObservers("Any change");

        // Assert
        observerMock.Verify(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()), Times.Never);
    }

    [Fact]
    public void Attach_WhenObserverAttachedTwice_ReceivesSingleNotification()
    {
        // Arrange
        var backlogItem = new Domain.Entities.BacklogItem();
        var observerMock = new Mock<IObserver>();
        observerMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        backlogItem.Attach(observerMock.Object);

        // Act
        backlogItem.Attach(observerMock.Object);
        backlogItem.NotifyObservers("State changed");

        // Assert
        observerMock.Verify(o => o.Update("State changed", null), Times.Once);
    }
}
