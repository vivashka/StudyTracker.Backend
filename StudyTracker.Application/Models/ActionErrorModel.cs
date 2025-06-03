namespace StudyTracker.Application.Models;

public sealed record ActionErrorModel(
    int ErrorCode, 
    string ErrorMessage);