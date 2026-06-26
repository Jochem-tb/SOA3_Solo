using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.State;

namespace AvansDevOps.Tests;

public class CommentTests
{
    [Fact]
    public void SendMessage_WhenCommentProvided_PreservesCommentData()
    {
        // Arrange
        var developer = new User { Name = "Bob" };
        var item = new BacklogItem { AssignedDeveloper = developer, State = new DoingState(new BacklogItem()) };
        var contextItem = new BacklogItem { AssignedDeveloper = developer };
        contextItem.State = new DoingState(contextItem);
        var discussion = contextItem.State.CreateDiscussion();
        var expectedTime = new DateTime(2026, 1, 1, 10, 0, 0, DateTimeKind.Utc);
        var comment = new Domain.Entities.Comment
        {
            User = developer,
            Time = expectedTime,
            Message = "Please review API output."
        };

        // Act
        contextItem.State.SendMessage(discussion, comment);

        // Assert
        Assert.Equal("Please review API output.", discussion.Messages[0].Message);
        Assert.Same(developer, discussion.Messages[0].User);
        Assert.Equal(expectedTime, discussion.Messages[0].Time);
    }
}
