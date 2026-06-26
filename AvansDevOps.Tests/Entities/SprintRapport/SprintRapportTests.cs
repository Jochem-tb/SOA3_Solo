using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;
using Moq;

namespace AvansDevOps.Tests;

public class SprintRapportTests
{
    [Fact]
    public void Constructor_WhenExportStrategyIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IExportStrategy strategy = null!;

        // Act
        void Act() => new Domain.Entities.SprintRapport(strategy);

        // Assert
        Assert.Throws<ArgumentNullException>(Act);
    }

    [Fact]
    public void Constructor_WhenExportStrategyIsProvided_AssignsStrategy()
    {
        // Arrange
        var strategyMock = new Mock<IExportStrategy>();
        strategyMock.Setup(s => s.Export());

        // Act
        var rapport = new Domain.Entities.SprintRapport(strategyMock.Object);
        rapport.ExportStrategy.Export();

        // Assert
        Assert.Same(strategyMock.Object, rapport.ExportStrategy);
        strategyMock.Verify(s => s.Export(), Times.Once);
    }
}
