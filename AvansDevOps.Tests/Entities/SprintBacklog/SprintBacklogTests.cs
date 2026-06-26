using AvansDevOps.Domain.Entities;

namespace AvansDevOps.Tests;

public class SprintBacklogTests
{
    [Fact]
    public void AddItem_WhenItemIsNull_DoesNotAddItem()
    {
        // Arrange
        var backlog = new Domain.Entities.SprintBacklog();

        // Act
        backlog.AddItem(null!);

        // Assert
        Assert.Empty(backlog.Items);
    }

    [Fact]
    public void AddItem_WhenItemAlreadyExists_DoesNotDuplicateItem()
    {
        // Arrange
        var backlog = new Domain.Entities.SprintBacklog();
        var item = CreateDoneItem("Story A");
        backlog.AddItem(item);

        // Act
        backlog.AddItem(item);

        // Assert
        Assert.Single(backlog.Items);
    }

    [Fact]
    public void RemoveItem_WhenItemDoesNotExist_DoesNotThrowAndKeepsBacklog()
    {
        // Arrange
        var backlog = new Domain.Entities.SprintBacklog();
        backlog.AddItem(CreateDoneItem("Story A"));
        var missingItem = CreateDoneItem("Story B");

        // Act
        var exception = Record.Exception(() => backlog.RemoveItem(missingItem));

        // Assert
        Assert.Null(exception);
        Assert.Single(backlog.Items);
    }

    [Fact]
    public void IsDone_WhenBacklogIsEmpty_ReturnsFalse()
    {
        // Arrange
        var backlog = new Domain.Entities.SprintBacklog();

        // Act
        var result = backlog.IsDone();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsDone_WhenAllItemsAreDone_ReturnsTrue()
    {
        // Arrange
        var backlog = new Domain.Entities.SprintBacklog();
        backlog.AddItem(CreateDoneItem("Story A"));
        backlog.AddItem(CreateDoneItem("Story B"));

        // Act
        var result = backlog.IsDone();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsDone_WhenAnyItemIsNotDone_ReturnsFalse()
    {
        // Arrange
        var backlog = new Domain.Entities.SprintBacklog();
        backlog.AddItem(CreateDoneItem("Story A"));
        backlog.AddItem(CreateUndoneItem("Story B"));

        // Act
        var result = backlog.IsDone();

        // Assert
        Assert.False(result);
    }

    private static AvansDevOps.Domain.Entities.BacklogItem CreateDoneItem(string title)
    {
        var item = new AvansDevOps.Domain.Entities.BacklogItem { Title = title };
        var activity = new Activity { Description = "Done" };
        activity.MarkAsDone();
        item.Add(activity);
        return item;
    }

    private static AvansDevOps.Domain.Entities.BacklogItem CreateUndoneItem(string title)
    {
        var item = new AvansDevOps.Domain.Entities.BacklogItem { Title = title };
        item.Add(new Activity { Description = "Todo" });
        return item;
    }
}
