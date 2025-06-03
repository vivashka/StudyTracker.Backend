using Dapper;
using StudyTracker.Domain.Models;
using StudyTracker.Infrastructure.Postgres.Contracts;

namespace StudyTracker.Infrastructure.Postgres.Repository;

public class CoursesRepository : BaseRepository, ICoursesRepository
{
    public Task<Course[]> GetCoursesByStudent(Guid studentId, CancellationToken cancellationToken)
    {
        string sqlRequest = """
                            SELECT *
                            FROM "Courses"
                            LEFT JOIN "StudentsCourses" sc ON sc."StudentId" = @StudentId
                            """;

        var param = new DynamicParameters();
        
        param.Add("StudentId", studentId);

        return ExecuteQueryAsync<Course>(sqlRequest, param, cancellationToken);
    }

    public Task<Course[]> GetCourses(CancellationToken cancellationToken)
    {
        string sqlRequest = """
                            SELECT * FROM "Courses"
                            """;

        var param = new DynamicParameters();
        
        return ExecuteQueryAsync<Course>(sqlRequest, param, cancellationToken);
    }
}