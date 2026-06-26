using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Interfaces;
using Moq;

namespace AvansDevOps.Tests;

public class DevelopmentPipelineTests
{
    [Fact]
    public void AddAction_WhenActionIsNull_DoesNotAddAction()
    {
        // Arrange
        var pipeline = new DevelopmentPipeline();

        // Act
        pipeline.AddAction(null!);

        // Assert
        Assert.Empty(pipeline.Pipeline);
    }

    [Fact]
    public void RemoveAction_WhenIndexIsInvalid_DoesNotRemoveAnyAction()
    {
        // Arrange
        var pipeline = new DevelopmentPipeline();
        var actionMock = new Mock<IPipelineAction>();
        actionMock.Setup(a => a.Execute()).Returns(true);
        actionMock.Setup(a => a.Rollback()).Returns(true);
        pipeline.AddAction(actionMock.Object);

        // Act
        pipeline.RemoveAction(2);

        // Assert
        Assert.Single(pipeline.Pipeline);
    }

    [Fact]
    public void RemoveAction_WhenIndexIsValid_RemovesAction()
    {
        // Arrange
        var pipeline = new DevelopmentPipeline();
        var actionMock = new Mock<IPipelineAction>();
        actionMock.Setup(a => a.Execute()).Returns(true);
        actionMock.Setup(a => a.Rollback()).Returns(true);
        pipeline.AddAction(actionMock.Object);

        // Act
        pipeline.RemoveAction(0);

        // Assert
        Assert.Empty(pipeline.Pipeline);
    }

    [Fact]
    public void Execute_WhenPipelineIsEmpty_ReturnsTrue()
    {
        // Arrange
        var pipeline = new DevelopmentPipeline();

        // Act
        var result = pipeline.Execute();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Execute_WhenAllActionsSucceed_ReturnsTrue()
    {
        // Arrange
        var pipeline = new DevelopmentPipeline();
        var firstActionMock = new Mock<IPipelineAction>();
        var secondActionMock = new Mock<IPipelineAction>();
        firstActionMock.Setup(a => a.Execute()).Returns(true);
        secondActionMock.Setup(a => a.Execute()).Returns(true);
        pipeline.AddAction(firstActionMock.Object);
        pipeline.AddAction(secondActionMock.Object);

        // Act
        var result = pipeline.Execute();

        // Assert
        Assert.True(result);
        firstActionMock.Verify(a => a.Execute(), Times.Once);
        secondActionMock.Verify(a => a.Execute(), Times.Once);
    }

    [Fact]
    public void Execute_WhenActionFails_RollsBackExecutedActionsInReverseOrderAndReturnsFalse()
    {
        // Arrange
        var pipeline = new DevelopmentPipeline();
        var callOrder = new List<string>();

        var firstActionMock = new Mock<IPipelineAction>();
        firstActionMock.Setup(a => a.Execute()).Callback(() => callOrder.Add("execute-1")).Returns(true);
        firstActionMock.Setup(a => a.Rollback()).Callback(() => callOrder.Add("rollback-1")).Returns(true);

        var secondActionMock = new Mock<IPipelineAction>();
        secondActionMock.Setup(a => a.Execute()).Callback(() => callOrder.Add("execute-2")).Returns(false);

        pipeline.AddAction(firstActionMock.Object);
        pipeline.AddAction(secondActionMock.Object);

        // Act
        var result = pipeline.Execute();

        // Assert
        Assert.False(result);
        Assert.Equal(new[] { "execute-1", "execute-2", "rollback-1" }, callOrder);
    }

    [Fact]
    public void Execute_WhenActionThrows_RollsBackExecutedActionsInReverseOrderAndRethrows()
    {
        // Arrange
        var pipeline = new DevelopmentPipeline();
        var callOrder = new List<string>();

        var firstActionMock = new Mock<IPipelineAction>();
        firstActionMock.Setup(a => a.Execute()).Callback(() => callOrder.Add("execute-1")).Returns(true);
        firstActionMock.Setup(a => a.Rollback()).Callback(() => callOrder.Add("rollback-1")).Returns(true);

        var secondActionMock = new Mock<IPipelineAction>();
        secondActionMock.Setup(a => a.Execute()).Callback(() => callOrder.Add("execute-2")).Throws(new InvalidOperationException("Boom"));

        pipeline.AddAction(firstActionMock.Object);
        pipeline.AddAction(secondActionMock.Object);

        // Act
        void Act() => pipeline.Execute();

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
        Assert.Equal(new[] { "execute-1", "execute-2", "rollback-1" }, callOrder);
    }
}
