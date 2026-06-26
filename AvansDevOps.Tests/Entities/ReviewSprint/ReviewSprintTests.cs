using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;
using Moq;

namespace AvansDevOps.Tests;

public class ReviewSprintTests
{
    [Fact]
    public void FinishSprint_WhenCalled_NotifiesFinishMessage()
    {
        // Arrange
        var sprint = new Domain.Entities.ReviewSprint();
        var observerMock = new Mock<IObserver>();
        observerMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        sprint.Attach(observerMock.Object);

        // Act
        sprint.FinishSprint();

        // Assert
        observerMock.Verify(o => o.Update("ReviewSprint finished", null), Times.Once);
    }

    [Fact]
    public void FinalizeClosingOfSprint_WhenCalled_NotifiesFinalizeMessage()
    {
        // Arrange
        var sprint = new Domain.Entities.ReviewSprint();
        var observerMock = new Mock<IObserver>();
        observerMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        sprint.Attach(observerMock.Object);

        // Act
        sprint.FinalizeClosingOfSprint();

        // Assert
        observerMock.Verify(o => o.Update("ReviewSprint finalized", null), Times.Once);
    }
}
