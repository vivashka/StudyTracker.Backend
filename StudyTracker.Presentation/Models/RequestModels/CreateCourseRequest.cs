using StudyTracker.Domain.Models;

namespace StudyTracker.Presentation.Models.RequestModels;

public record CreateCourseRequest(Course Course, Guid StudentId);