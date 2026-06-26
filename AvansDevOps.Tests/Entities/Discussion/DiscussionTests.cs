using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.State;

namespace AvansDevOps.Tests;

public class DiscussionTests
{
    [Fact]
    public void SendMessage_WhenCalledTwice_PreservesMessageOrder()
    {
        // Arrange
        var item = new BacklogItem();
        item.State = new DoingState(item);
        var discussion = item.State.CreateDiscussion();
        var first = new Comment { Message = "First" };
        var second = new Comment { Message = "Second" };

        // Act
        item.State.SendMessage(discussion, first);
        item.State.SendMessage(discussion, second);

        // Assert
        Assert.Equal(2, discussion.Messages.Count);
        Assert.Equal("First", discussion.Messages[0].Message);
        Assert.Equal("Second", discussion.Messages[1].Message);
    }
}
