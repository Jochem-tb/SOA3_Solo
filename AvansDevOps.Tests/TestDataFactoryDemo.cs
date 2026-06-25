using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Enums;
using AvansDevOps.Infrastructure.Fakes;

namespace AvansDevOps.Tests;

public class TestDataFactoryDemo
{
    [Fact]
    public void CreateUserWithPreferences_SetsPreferences()
    {
        var factory = new TestDataFactory();

        var user = factory.CreateUserWithPreferences("Jane PO", NotificationPreference.Email, NotificationPreference.Slack);

        Assert.Equal("Jane PO", user.Name);
        Assert.Contains(NotificationPreference.Email, user.NotificationPreferences);
        Assert.Contains(NotificationPreference.Slack, user.NotificationPreferences);
    }

    [Fact]
    public void BacklogItemWithActivities_IsDoneAfterAllChildrenCompleted()
    {
        var factory = new TestDataFactory();
        var user = factory.CreateUser("Dev");
        var backlogItem = factory.CreateBacklogItemWithActivities("Authentication", 3, user);

        Assert.False(backlogItem.IsDone());

        foreach (var child in backlogItem.Children.OfType<Activity>())
        {
            child.MarkAsDone();
        }

        Assert.True(backlogItem.IsDone());
    }

    [Fact]
    public void CreateCompleteTestScenario_CreatesConnectedGraph()
    {
        var factory = new TestDataFactory();

        var scenario = factory.CreateCompleteTestScenario();

        Assert.NotNull(scenario.Project);
        Assert.NotNull(scenario.ProductOwner);
        Assert.NotNull(scenario.Developers);
        Assert.Equal(2, scenario.Developers.Length);
        Assert.NotNull(scenario.BacklogItems);
        Assert.Equal(3, scenario.BacklogItems.Length);
        Assert.NotNull(scenario.SprintBacklog);
        Assert.Equal(2, scenario.SprintBacklog.Items.Count);
        Assert.Equal(3, scenario.Project!.ProductBacklog.Items.Count);
    }
}