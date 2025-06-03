using StudyTracker.Application.Contracts.External;
using StudyTracker.Domain.Models;
using StudyTracker.Infrastructure.Postgres.Contracts;
using StudyTracker.Infrastructure.Postgres.Repository;

namespace StudyTracker.Infrastructure.Postgres.Services;

public class CoursesProvider(ICoursesRepository coursesRepository) : ICoursesProvider
{
    public async Task<Course[]> GetCoursesByStudent(Guid studentId, CancellationToken cancellationToken)
    {
        try
        {
            var courses = await coursesRepository.GetCoursesByStudent(studentId, cancellationToken);

            return courses;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return [];
        }
    }

    public async Task<Course[]> GetCourses(CancellationToken cancellationToken)
    {
        try
        {
            var courses = await coursesRepository.GetCourses(cancellationToken);

            return courses;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return [];
        }
    }
}