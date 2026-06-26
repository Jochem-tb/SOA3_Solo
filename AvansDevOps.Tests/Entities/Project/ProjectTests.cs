using AvansDevOps.Domain.Entities;

namespace AvansDevOps.Tests;

public class ProjectTests
{
    [Fact]
    public void AddSprint_WhenSprintIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var project = new Domain.Entities.Project();

        // Act
        void Act() => project.AddSprint(null!);

        // Assert
        Assert.Throws<ArgumentNullException>(Act);
    }

    [Fact]
    public void AddSprint_WhenSprintAlreadyAdded_DoesNotDuplicate()
    {
        // Arrange
        var project = new Domain.Entities.Project();
        var sprint = new ReviewSprint();
        project.AddSprint(sprint);

        // Act
        project.AddSprint(sprint);

        // Assert
        Assert.Single(project.Sprints);
    }

    [Fact]
    public void AddBacklogItem_WhenItemIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var project = new Domain.Entities.Project();

        // Act
        void Act() => project.AddBacklogItem(null!);

        // Assert
        Assert.Throws<ArgumentNullException>(Act);
    }

    [Fact]
    public void AddBacklogItem_WhenItemIsValid_AddsToProductBacklog()
    {
        // Arrange
        var project = new Domain.Entities.Project();
        var item = new AvansDevOps.Domain.Entities.BacklogItem { Title = "Authentication" };

        // Act
        project.AddBacklogItem(item);

        // Assert
        Assert.Single(project.ProductBacklog.Items);
    }
}
