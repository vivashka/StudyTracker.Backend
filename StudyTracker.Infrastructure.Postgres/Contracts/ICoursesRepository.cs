using StudyTracker.Domain.Models;

namespace StudyTracker.Infrastructure.Postgres.Contracts;

public interface ICoursesRepository
{
    public Task<Course[]> GetCoursesByStudent(Guid studentId, CancellationToken cancellationToken);
    public Task<Course[]> GetCourses(CancellationToken cancellationToken);
}