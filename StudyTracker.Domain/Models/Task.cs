using StudyTracker.Domain.Enums;

namespace StudyTracker.Domain.Models;

public class Task
{
    public Guid TaskId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public TaskState State { get; init; }
}