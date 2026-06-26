using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;
using Moq;

namespace AvansDevOps.Tests;

public class UserTests
{
    [Fact]
    public void NotifyObservers_WhenAssignedDeveloperExists_PassesThatUserToObserver()
    {
        // Arrange
        var expectedUser = new Domain.Entities.User { Name = "Alice" };
        var item = new BacklogItem { AssignedDeveloper = expectedUser };
        var observerMock = new Mock<IObserver>();
        observerMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        item.Attach(observerMock.Object);

        // Act
        item.NotifyObservers("Item changed");

        // Assert
        observerMock.Verify(o => o.Update("Item changed", expectedUser), Times.Once);
    }
}
