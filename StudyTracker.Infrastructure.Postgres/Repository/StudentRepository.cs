using Dapper;
using StudyTracker.Domain.Models;
using StudyTracker.Infrastructure.Postgres.Contracts;

namespace StudyTracker.Infrastructure.Postgres.Repository;

public class StudentRepository : BaseRepository, IStudentRepository
{
    public async Task<Student> Authenticate(string login, CancellationToken cancellationToken)
    {
        string sqlRequest = """
                            SELECT *
                            FROM "Students"
                            WHERE "Login" = @Login
                            """;

        var param = new DynamicParameters();
        param.Add("Login", login);

        return await ExecuteQuerySingleAsync<Student>(sqlRequest, param, cancellationToken);
    }

    public async Task<Student> Registration(string login, string password, CancellationToken cancellationToken)
    {
        string sqlRequest = """
                            INSERT INTO "Students"
                            VALUES (@login, @Password)
                            """;

        var param = new DynamicParameters();
        param.Add("Login", login);
        param.Add("Password", password);

        return await ExecuteQuerySingleAsync<Student>(sqlRequest, param, cancellationToken);
    }

    public async Task<Student[]> GetUsers(CancellationToken cancellationToken)
    {
        string sqlRequest = """
                            SELECT *
                            FROM "Students"
                            """;

        var param = new DynamicParameters();

        return await ExecuteQueryAsync<Student>(sqlRequest, param, cancellationToken);
    }
    
    public async Task<Student[]> GetUsersByCourse(Guid courseId, CancellationToken cancellationToken)
    {
        string sqlRequest = """
                            SELECT *
                            FROM "Students"
                            LEFT JOIN "StudentsCourses" sc ON sc."CourseId" = @CourseId
                            """;

        var param = new DynamicParameters();
        param.Add("CourseId", courseId);

        return await ExecuteQueryAsync<Student>(sqlRequest, param, cancellationToken);
    }
}