using AvansDevOps.Domain.Entities;
using AvansDevOps.Domain.Enums;
using AvansDevOps.Domain.Interfaces;

namespace AvansDevOps.Infrastructure.Fakes;

/// <summary>
/// Factory class for creating test/domain objects with logging.
/// This class ensures all objects are created in a consistent, testable way.
/// </summary>
public class TestDataFactory
{
    private readonly ITestLogger _logger;

    public TestDataFactory(ITestLogger? logger = null)
    {
        _logger = logger ?? new ConsoleTestLogger();
    }

    /// <summary>
    /// Creates a test user with default values.
    /// </summary>
    public User CreateUser(string? name = null, string? email = null)
    {
        var user = new User
        {
            Name = name ?? $"User_{Guid.NewGuid().ToString().Substring(0, 8)}",
            Email = email ?? $"test_{Guid.NewGuid().ToString().Substring(0, 8)}@example.com",
            NotificationPreferences = []
        };

        _logger.LogCreated("User", user.Id, $"Name: {user.Name}, Email: {user.Email}");
        return user;
    }

    /// <summary>
    /// Creates a user with specific notification preferences.
    /// </summary>
    public User CreateUserWithPreferences(
        string? name = null,
        params NotificationPreference[] preferences)
    {
        var user = CreateUser(name);
        user.NotificationPreferences.AddRange(preferences);
        
        var prefsStr = string.Join(", ", preferences);
        _logger.LogAction("AddPreferences", $"User {user.Id}: {prefsStr}");
        
        return user;
    }

    /// <summary>
    /// Creates a test activity.
    /// </summary>
    public Activity CreateActivity(string? description = null, User? assignee = null)
    {
        var activity = new Activity
        {
            Description = description ?? $"Activity_{Guid.NewGuid().ToString().Substring(0, 8)}",
            AssignedDeveloper = assignee,
            DoneStatus = false
        };

        _logger.LogCreated("Activity", activity.Id,
            $"Description: {activity.Description}, Assigned: {(assignee?.Name ?? "None")}");
        
        return activity;
    }

    /// <summary>
    /// Creates a test activity marked as done.
    /// </summary>
    public Activity CreateCompletedActivity(string? description = null, User? assignee = null)
    {
        var activity = CreateActivity(description, assignee);
        activity.MarkAsDone();
        
        _logger.LogAction("MarkAsDone", $"Activity {activity.Id}");
        
        return activity;
    }

    /// <summary>
    /// Creates a test backlog item with optional children.
    /// </summary>
    public BacklogItem CreateBacklogItem(
        string? title = null,
        string? description = null,
        User? assignee = null)
    {
        var item = new BacklogItem
        {
            Title = title ?? $"Story_{Guid.NewGuid().ToString().Substring(0, 8)}",
            Description = description ?? "Test backlog item",
            AssignedDeveloper = assignee
        };

        _logger.LogCreated("BacklogItem", item.Id,
            $"Title: {item.Title}, Assigned: {(assignee?.Name ?? "None")}");
        
        return item;
    }

    /// <summary>
    /// Adds a child work item to a backlog item and logs the action.
    /// </summary>
    public void AddChildToBacklogItem(BacklogItem parent, IWorkItem child)
    {
        parent.Add(child);
        _logger.LogAction("AddChild", $"Parent {parent.Id} <- Child {GetWorkItemId(child)}");
    }

    /// <summary>
    /// Creates a complete backlog item with activities as children.
    /// </summary>
    public BacklogItem CreateBacklogItemWithActivities(
        string? title = null,
        int activityCount = 3,
        User? assignee = null)
    {
        var backlogItem = CreateBacklogItem(title, assignee: assignee);
        
        for (int i = 0; i < activityCount; i++)
        {
            var activity = CreateActivity($"Activity_{i + 1}", assignee);
            backlogItem.Add(activity);
        }

        _logger.LogAction("CreateWithActivities",
            $"BacklogItem {backlogItem.Id} with {activityCount} activities");
        
        return backlogItem;
    }

    /// <summary>
    /// Creates a sprint backlog.
    /// </summary>
    public SprintBacklog CreateSprintBacklog()
    {
        var backlog = new SprintBacklog();
        _logger.LogCreated("SprintBacklog", backlog.Id, "Empty sprint backlog");
        return backlog;
    }

    /// <summary>
    /// Creates a product backlog.
    /// </summary>
    public ProductBacklog CreateProductBacklog()
    {
        var backlog = new ProductBacklog();
        _logger.LogCreated("ProductBacklog", backlog.Id, "Empty product backlog");
        return backlog;
    }

    /// <summary>
    /// Creates a project with optional product owner and repository.
    /// </summary>
    public Project CreateProject(
        string? name = null,
        User? productOwner = null)
    {
        var project = new Project
        {
            Name = name ?? $"Project_{Guid.NewGuid().ToString().Substring(0, 8)}",
            ProductOwner = productOwner,
            Description = "Test project"
        };

        _logger.LogCreated("Project", project.Id,
            $"Name: {project.Name}, Owner: {(productOwner?.Name ?? "None")}");
        
        return project;
    }

    /// <summary>
    /// Adds a backlog item to a project's product backlog and logs the action.
    /// </summary>
    public void AddBacklogItemToProject(Project project, BacklogItem item)
    {
        project.ProductBacklog.AddItem(item);
        _logger.LogAction("AddToProductBacklog",
            $"Project {project.Id} <- BacklogItem {item.Id}");
    }

    /// <summary>
    /// Gets the ID of a work item (polymorphic helper).
    /// </summary>
    private string GetWorkItemId(IWorkItem item)
    {
        return item switch
        {
            Activity activity => activity.Id,
            BacklogItem backlogItem => backlogItem.Id,
            _ => "Unknown"
        };
    }

    /// <summary>
    /// Creates a complete test scenario with users, backlog items, and activities.
    /// </summary>
    public TestScenario CreateCompleteTestScenario()
    {
        _logger.Log("=== CREATING COMPLETE TEST SCENARIO ===");

        // Create users
        var productOwner = CreateUser("Alice (PO)");
        var developer1 = CreateUserWithPreferences("Bob (Dev)", NotificationPreference.Email, NotificationPreference.Slack);
        var developer2 = CreateUserWithPreferences("Carol (Dev)", NotificationPreference.SMS);

        // Create project
        var project = CreateProject("E-Commerce Platform", productOwner);

        // Create backlog items with activities
        var story1 = CreateBacklogItemWithActivities("User Authentication", 3, developer1);
        var story2 = CreateBacklogItemWithActivities("Payment Processing", 4, developer2);
        var story3 = CreateBacklogItem("API Documentation");

        // Add to project
        AddBacklogItemToProject(project, story1);
        AddBacklogItemToProject(project, story2);
        AddBacklogItemToProject(project, story3);

        // Create sprint
        var sprintBacklog = CreateSprintBacklog();
        sprintBacklog.AddItem(story1);
        sprintBacklog.AddItem(story2);

        _logger.Log("=== TEST SCENARIO COMPLETE ===");

        return new TestScenario
        {
            ProductOwner = productOwner,
            Developers = new[] { developer1, developer2 },
            Project = project,
            BacklogItems = new[] { story1, story2, story3 },
            SprintBacklog = sprintBacklog
        };
    }

    /// <summary>
    /// Prints a summary of all logs to console.
    /// </summary>
    public void PrintLogSummary()
    {
        Console.WriteLine("\n╔════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║               TEST DATA FACTORY LOG SUMMARY                ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

        var logs = _logger.GetLogs();
        foreach (var log in logs)
        {
            Console.WriteLine(log);
        }

        Console.WriteLine($"\n📊 Total Operations: {logs.Count}");
    }

    /// <summary>
    /// Clears all logs.
    /// </summary>
    public void ClearLogs()
    {
        _logger.Clear();
    }

    /// <summary>
    /// Gets all logs from the logger.
    /// </summary>
    public IReadOnlyList<string> GetLogs() => _logger.GetLogs();
}

/// <summary>
/// Represents a complete test scenario with all related entities.
/// </summary>
public class TestScenario
{
    public User? ProductOwner { get; set; }
    public User[]? Developers { get; set; }
    public Project? Project { get; set; }
    public BacklogItem[]? BacklogItems { get; set; }
    public SprintBacklog? SprintBacklog { get; set; }
}
