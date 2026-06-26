using AvansDevOps.Domain.Entities;

namespace AvansDevOps.Tests;

public class ProductBacklogTests
{
    [Fact]
    public void AddItem_WhenItemIsValid_AddsItem()
    {
        // Arrange
        var backlog = new Domain.Entities.ProductBacklog();
        var item = new AvansDevOps.Domain.Entities.BacklogItem { Title = "Story A" };

        // Act
        backlog.AddItem(item);

        // Assert
        Assert.Single(backlog.Items);
    }

    [Fact]
    public void AddItem_WhenItemAlreadyExists_DoesNotDuplicate()
    {
        // Arrange
        var backlog = new Domain.Entities.ProductBacklog();
        var item = new AvansDevOps.Domain.Entities.BacklogItem { Title = "Story A" };
        backlog.AddItem(item);

        // Act
        backlog.AddItem(item);

        // Assert
        Assert.Single(backlog.Items);
    }

    [Fact]
    public void RemoveItem_WhenItemExists_RemovesItem()
    {
        // Arrange
        var backlog = new Domain.Entities.ProductBacklog();
        var item = new AvansDevOps.Domain.Entities.BacklogItem { Title = "Story A" };
        backlog.AddItem(item);

        // Act
        backlog.RemoveItem(item);

        // Assert
        Assert.Empty(backlog.Items);
    }
}
