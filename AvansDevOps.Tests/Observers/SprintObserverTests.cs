using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;
using Moq;

namespace AvansDevOps.Tests;

public class SprintObserverTests
{
    [Fact]
    public void FinishSprint_WhenSprintIsReview_NotifiesObserversWithReviewMessage()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var observerMock = new Mock<IObserver>();
        observerMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        sprint.Attach(observerMock.Object);

        // Act
        sprint.FinishSprint();

        // Assert
        observerMock.Verify(o => o.Update("ReviewSprint finished", null), Times.Once);
    }

    [Fact]
    public void FinalizeClosingOfSprint_WhenSprintIsReview_NotifiesObserversWithReviewFinalizeMessage()
    {
        // Arrange
        var sprint = new ReviewSprint();
        var observerMock = new Mock<IObserver>();
        observerMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        sprint.Attach(observerMock.Object);

        // Act
        sprint.FinalizeClosingOfSprint();

        // Assert
        observerMock.Verify(o => o.Update("ReviewSprint finalized", null), Times.Once);
    }

    [Fact]
    public void FinalizeClosingOfSprint_WhenReleaseSprintHasPipeline_NotifiesPipelineReadyMessage()
    {
        // Arrange
        var sprint = new ReleaseSprint
        {
            DeploymentPipeline = new DevelopmentPipeline()
        };
        var observerMock = new Mock<IObserver>();
        observerMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        sprint.Attach(observerMock.Object);

        // Act
        sprint.FinalizeClosingOfSprint();

        // Assert
        observerMock.Verify(o => o.Update("ReleaseSprint finalized - deployment pipeline ready", null), Times.Once);
    }

    [Fact]
    public void FinalizeClosingOfSprint_WhenReleaseSprintHasNoPipeline_NotifiesDefaultFinalizeMessage()
    {
        // Arrange
        var sprint = new ReleaseSprint();
        var observerMock = new Mock<IObserver>();
        observerMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        sprint.Attach(observerMock.Object);

        // Act
        sprint.FinalizeClosingOfSprint();

        // Assert
        observerMock.Verify(o => o.Update("ReleaseSprint finalized", null), Times.Once);
    }
}
