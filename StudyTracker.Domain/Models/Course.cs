namespace StudyTracker.Domain.Models;

public class Course
{
    public Guid? CourseId { get; init; }
    public string Name { get; init; }
    public string Professor { get; init; }
    public string Description { get; init; }
}