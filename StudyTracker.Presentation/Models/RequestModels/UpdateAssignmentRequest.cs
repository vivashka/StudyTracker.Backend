using StudyTracker.Domain.Models;

namespace StudyTracker.Presentation.Models.RequestModels;

public record UpdateAssignmentRequest(Assignments Assignments, Guid? StudentId);