namespace StudyTracker.Domain.Models;

public record Student
{
    public Guid StudentId { get; init; }
    public string Login { get; init; }
    public string Password { get; init; }
    
    public bool IsAdmin { get; set; }
}