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

    public async Task<Course> CreateCourse(Course course, CancellationToken cancellationToken)
    {

        Guid courseId = course.CourseId ?? Guid.NewGuid();
        string sqlRequest = """
                            INSERT INTO "Courses"
                            VALUES (@CourseId, @Name, @Professor, @Description)
                            ON CONFLICT ("Name")
                            DO UPDATE SET 
                                "Professor" = EXCLUDED."Professor"
                                "Description" = EXCLUDED."Description"
                            RETURNING *;
                            """;

        var param = new DynamicParameters(course);
        param.Add("CourseId", courseId);

        return await ExecuteQuerySingleAsync<Course>(sqlRequest, param, cancellationToken);
    }

    public async Task<Guid> AssignCourse(Guid courseId, Guid studentId, CancellationToken cancellationToken)
    {
        string sqlRequest = """
                            INSERT INTO "StudentsCourses"
                            VALUES (@StudentId, @CourseId)
                                ON CONFLICT ("StudentId", "CourseId") DO NOTHING
                            RETURNING "StudentId";
                            """;

        var param = new DynamicParameters();
        param.Add("StudentId", studentId);
        param.Add("CourseId", courseId);

        return await ExecuteQuerySingleAsync<Guid>(sqlRequest, param, cancellationToken);
    }

    public async Task<Course> DeleteCourse(Guid courseId, CancellationToken cancellationToken)
    {
        string sqlRequest = """
                            DELETE FROM "Courses"
                            WHERE "AssignmentId" = @CourseId
                            RETURNING *;
                            """;

       
        var param = new DynamicParameters();
        param.Add("CourseId", courseId);
        
        return await ExecuteQuerySingleAsync<Course>(sqlRequest, param, cancellationToken);
    }
}