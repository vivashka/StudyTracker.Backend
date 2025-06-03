namespace StudyTracker.Presentation.Models;

public record AuthModel
{
    public string Login { get; init; }
    
    public string Password { get; init; }
}