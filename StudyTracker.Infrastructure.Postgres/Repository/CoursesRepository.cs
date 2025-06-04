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
                            WITH updated AS (
                                UPDATE "Courses"
                                SET "Name" = @Name, "Professor" = @Professor, "Description" = @Description
                                WHERE "CourseId" = @CourseId
                                RETURNING *
                            ),
                            inserted AS (
                                INSERT INTO "Courses" ("CourseId", "Name", "Professor", "Description")
                                SELECT @CourseId, @Name, @Professor, @Description
                                WHERE NOT EXISTS (SELECT 1 FROM updated)
                                ON CONFLICT ("Name")
                                DO UPDATE SET
                                    "Professor" = EXCLUDED."Professor",
                                    "Description" = EXCLUDED."Description"
                                RETURNING *
                            )
                            SELECT * FROM updated
                            UNION ALL
                            SELECT * FROM inserted;
                            """;
        
        var param = new DynamicParameters(course);
        param.Add("CourseId", courseId);
        param.Add("Name", course.Name);
        param.Add("Description", course.Description);
        param.Add("Professor", course.Professor);
        

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
                            WHERE "CourseId" = @CourseId
                            RETURNING *;
                            """;

       
        var param = new DynamicParameters();
        param.Add("CourseId", courseId);
        
        return await ExecuteQuerySingleAsync<Course>(sqlRequest, param, cancellationToken);
    }
}