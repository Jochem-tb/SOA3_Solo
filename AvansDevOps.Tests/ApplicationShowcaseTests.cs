using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Enums;
using AvansDevOps.Domain.Interfaces;
using AvansDevOps.Domain.State;
using AvansDevOps.Infrastructure.Decorators;

namespace AvansDevOps.Tests;

public class ApplicationShowcaseTests
{
    [Fact]
    public void ProjectAndSprintSetup_Works()
    {
        var productOwner = CreateUser("Alice PO", "alice@example.com", NotificationPreference.Email, NotificationPreference.Slack);
        var developer = CreateUser("Bob Dev", "bob@example.com", NotificationPreference.Slack);

        var project = new Project
        {
            Name = "Showcase Project",
            ProductOwner = productOwner
        };

        var loginItem = CreateBacklogItemWithActivities("Login Feature", "Implement login", developer, "Create login endpoint", "Create login UI");
        var profileItem = CreateBacklogItemWithActivities("Profile Feature", "Implement profile page", developer, "Create profile endpoint");

        project.ProductBacklog.AddItem(loginItem);
        project.ProductBacklog.AddItem(profileItem);
        Assert.Equal(2, project.ProductBacklog.Items.Count);

        var sprint = new ReleaseSprint
        {
            Name = "Sprint 1",
            StartDate = DateTime.UtcNow.Date,
            EndDate = DateTime.UtcNow.Date.AddDays(14)
        };

        project.Sprints.Add(sprint);
        Assert.Single(project.Sprints);

        Assert.IsType<CreatedState>(sprint.State);
        sprint.State!.AddBacklogItem(loginItem);
        sprint.State.AddBacklogItem(profileItem);
        Assert.Equal(2, sprint.Backlog.Items.Count);

        var createdState = Assert.IsType<CreatedState>(sprint.State);
        createdState.Activate();
        Assert.IsType<ActiveState>(sprint.State);
    }

    [Fact]
    public void BacklogItemWorkflowAndDiscussion_Works()
    {
        var developer = CreateUser("Bob Dev", "bob@example.com", NotificationPreference.Slack);
        var loginItem = CreateBacklogItemWithActivities("Login Feature", "Implement login", developer, "Create login endpoint", "Create login UI");

        var observer = new ShowcaseObserver();
        loginItem.Attach(observer);

        Assert.IsType<TodoState>(loginItem.State);
        loginItem.State!.MoveToDoing();

        var discussion = loginItem.State!.CreateDiscussion();
        loginItem.State.SendMessage(discussion, new Comment
        {
            Time = DateTime.UtcNow,
            User = developer,
            Message = "Work started, endpoint is ready for review soon."
        });

        Assert.Single(discussion.Messages);

        loginItem.State.MoveToReadyForTesting();
        loginItem.State.MoveToTesting();
        loginItem.State.MoveToTested();
        loginItem.State.MoveToDone();
        Assert.IsType<DoneState>(loginItem.State);

        foreach (var activity in loginItem.Children.OfType<Activity>())
        {
            activity.MarkAsDone();
        }

        Assert.True(loginItem.IsDone());
        Assert.True(observer.Messages.Count > 0);
    }

    [Fact]
    public void SprintClosurePipelineAndNotification_Works()
    {
        var productOwner = CreateUser("Alice PO", "alice@example.com", NotificationPreference.Email, NotificationPreference.Slack);
        var developer = CreateUser("Bob Dev", "bob@example.com", NotificationPreference.Slack);

        var sprint = new ReleaseSprint
        {
            Name = "Sprint 1",
            StartDate = DateTime.UtcNow.Date,
            EndDate = DateTime.UtcNow.Date.AddDays(14)
        };

        var loginItem = CreateBacklogItemWithActivities("Login Feature", "Implement login", developer, "Create login endpoint", "Create login UI");
        var profileItem = CreateBacklogItemWithActivities("Profile Feature", "Implement profile page", developer, "Create profile endpoint");

        sprint.Backlog.AddItem(loginItem);
        sprint.Backlog.AddItem(profileItem);

        MoveItemToDoneAndCompleteChildren(loginItem);
        MoveItemToDoneAndCompleteChildren(profileItem);

        sprint.CheckBacklogIsDone(sprint.Backlog);
        sprint.FinishSprint();
        sprint.FinalizeClosingOfSprint();

        if (sprint is ReleaseSprint releaseSprint)
        {
            releaseSprint.DeploymentPipeline = new DevelopmentPipeline();
            releaseSprint.DeploymentPipeline.AddAction(new FakePipelineAction());
            Assert.True(releaseSprint.DeploymentPipeline.Execute());
        }

        var notificationManager = new NotificationManager();
        notificationManager.Notify(productOwner, "Sprint completed successfully.");

        Assert.IsType<ReleaseSprint>(sprint);
        Assert.True(sprint.Backlog.IsDone());
    }

    private static User CreateUser(string name, string email, params NotificationPreference[] preferences)
    {
        return new User
        {
            Name = name,
            Email = email,
            NotificationPreferences = [.. preferences]
        };
    }

    private static BacklogItem CreateBacklogItemWithActivities(string title, string description, User developer, params string[] activityDescriptions)
    {
        var item = new BacklogItem
        {
            Title = title,
            Description = description,
            AssignedDeveloper = developer
        };

        foreach (var activityDescription in activityDescriptions)
        {
            item.Add(new Activity { Description = activityDescription, AssignedDeveloper = developer });
        }

        return item;
    }

    private static void MoveItemToDoneAndCompleteChildren(BacklogItem item)
    {
        item.State!.MoveToDoing();
        item.State.MoveToReadyForTesting();
        item.State.MoveToTesting();
        item.State.MoveToTested();
        item.State.MoveToDone();

        foreach (var activity in item.Children.OfType<Activity>())
        {
            activity.MarkAsDone();
        }
    }

    private sealed class ShowcaseObserver : IObserver
    {
        public List<string> Messages { get; } = [];

        public void Update(string context, User? user)
        {
            Messages.Add($"{context}:{user?.Name ?? "none"}");
        }
    }

    private sealed class FakePipelineAction : IPipelineAction
    {
        public bool Execute() => true;

        public bool Rollback() => true;
    }
}