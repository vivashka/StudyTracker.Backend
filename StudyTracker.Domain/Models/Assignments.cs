using StudyTracker.Domain.Enums;

namespace StudyTracker.Domain.Models;

public class Assignments
{
    public Guid AssignmentId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public TaskState State { get; init; }
}