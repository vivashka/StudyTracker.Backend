using StudyTracker.Domain.Models;

namespace StudyTracker.Application.Contracts.External;

public interface ICoursesProvider
{
    public Task<Course[]> GetCoursesByStudent(Guid studentId, CancellationToken cancellationToken);
    public Task<Course[]> GetCourses(CancellationToken cancellationToken);
}