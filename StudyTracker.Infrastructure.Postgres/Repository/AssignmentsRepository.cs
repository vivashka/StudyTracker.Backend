using Dapper;
using StudyTracker.Domain.Models;
using StudyTracker.Infrastructure.Postgres.Contracts;

namespace StudyTracker.Infrastructure.Postgres.Repository;

public class AssignmentsRepository : BaseRepository, IAssignmentsRepository
{
    public async Task<Assignments[]> GetAssignments(Guid studentId, Guid courseId, CancellationToken cancellationToken)
    {
        var sqlRequest = """
                         SELECT *
                         FROM "Assignments" a
                         JOIN "Courses" c ON c."Courseid" = @courseId
                         JOIN "StudentsCourses" sc ON c."Courseid" = sc.courseId
                         LEFT JOIN "AssignmentsStates" ast ON (
                             ast."StudentId" = @StudentId
                         )
                         """;
        
        var param = new DynamicParameters();
        param.Add("courseId", courseId);
        param.Add("StudentId", studentId);

        return await ExecuteQueryAsync<Assignments>(sqlRequest, param, cancellationToken);
    }

    public Task<Assignments[]> UpdateOrInsertAssignment(Guid studentId, Guid assignmentId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}