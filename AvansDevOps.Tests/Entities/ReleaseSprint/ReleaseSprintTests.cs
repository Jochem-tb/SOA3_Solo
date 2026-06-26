using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;
using Moq;

namespace AvansDevOps.Tests;

public class ReleaseSprintTests
{
    [Fact]
    public void FinishSprint_WhenCalled_NotifiesFinishMessage()
    {
        // Arrange
        var sprint = new Domain.Entities.ReleaseSprint();
        var observerMock = new Mock<IObserver>();
        observerMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        sprint.Attach(observerMock.Object);

        // Act
        sprint.FinishSprint();

        // Assert
        observerMock.Verify(o => o.Update("ReleaseSprint finished", null), Times.Once);
    }

    [Fact]
    public void FinalizeClosingOfSprint_WhenPipelineIsPresent_NotifiesPipelineReadyMessage()
    {
        // Arrange
        var sprint = new Domain.Entities.ReleaseSprint { DeploymentPipeline = new DevelopmentPipeline() };
        var observerMock = new Mock<IObserver>();
        observerMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        sprint.Attach(observerMock.Object);

        // Act
        sprint.FinalizeClosingOfSprint();

        // Assert
        observerMock.Verify(o => o.Update("ReleaseSprint finalized - deployment pipeline ready", null), Times.Once);
    }

    [Fact]
    public void FinalizeClosingOfSprint_WhenPipelineIsMissing_NotifiesDefaultFinalizeMessage()
    {
        // Arrange
        var sprint = new Domain.Entities.ReleaseSprint();
        var observerMock = new Mock<IObserver>();
        observerMock.Setup(o => o.Update(It.IsAny<string>(), It.IsAny<User?>()));
        sprint.Attach(observerMock.Object);

        // Act
        sprint.FinalizeClosingOfSprint();

        // Assert
        observerMock.Verify(o => o.Update("ReleaseSprint finalized", null), Times.Once);
    }
}
