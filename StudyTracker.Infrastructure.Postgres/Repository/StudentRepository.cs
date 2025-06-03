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
}