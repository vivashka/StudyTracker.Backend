using Dapper;
using StudyTracker.Domain.Enums;
using StudyTracker.Domain.Models;
using StudyTracker.Infrastructure.Postgres.Contracts;

namespace StudyTracker.Infrastructure.Postgres.Repository;

public class AssignmentsRepository : BaseRepository, IAssignmentsRepository
{
    public async Task<Assignments[]> GetAssignments(Guid studentId, Guid courseId, CancellationToken cancellationToken)
    {
        var sqlRequest = """
                         SELECT 
                             a."AssignmentId",
                             a."Name",
                             a."Description",
                             a."CourseId",
                             ast."States" AS "State",
                             a."Deadline"
                         FROM "Assignments" a
                         JOIN "Courses" c ON a."CourseId" = c."CourseId"
                         JOIN "StudentsCourses" sc ON c."CourseId" = sc."CourseId" AND sc."StudentId" = @StudentId
                         LEFT JOIN "AssignmentsStates" ast ON a."AssignmentId" = ast."AssignmentId" AND ast."StudentId" = @StudentId;
                         """;
        
        var param = new DynamicParameters();
        param.Add("CourseId", courseId);
        param.Add("StudentId", studentId);

        return await ExecuteQueryAsync<Assignments>(sqlRequest, param, cancellationToken);
    }

    public async Task<Assignments> UpdateOrInsertAssignment(Assignments assignment, CancellationToken cancellationToken)
    {

        Guid assignmentId = assignment.AssignmentId ?? Guid.NewGuid();
        var sqlRequest = """
                         INSERT INTO  "Assignments"
                         VALUES (@AssignmentId, @Name, @Description, @CourseId)
                            ON CONFLICT ("Name")
                         DO UPDATE SET 
                             "Description" = EXCLUDED."Description"
                         RETURNING *;
                         """;
        
        var param = new DynamicParameters(assignment);
        param.Add("AssignmentId", assignmentId);

        return await ExecuteQuerySingleAsync<Assignments>(sqlRequest, param, cancellationToken);
    }

    public async Task<int> UpdateOrInsertState(Guid assignmentId, Guid studentId, TaskState taskState, CancellationToken cancellationToken)
    {
        var sqlRequest = """
                         INSERT INTO  "AssignmentsStates"
                         VALUES (@AssignmentId, @StudentId, @TaskState)
                            ON CONFLICT ("AssignmentId", "StudentId") 
                         DO UPDATE SET "States" = @TaskState
                         RETURNING "States";
                         """;
        
        var param = new DynamicParameters();
        param.Add("AssignmentId", assignmentId);
        param.Add("StudentId", studentId);
        param.Add("TaskState", taskState);

        return await ExecuteQuerySingleAsync<int>(sqlRequest, param, cancellationToken);
    }

    public async Task<Assignments> DeleteAssignment(Guid assignmentId, CancellationToken cancellationToken)
    {
        string sqlRequest = """
                         DELETE FROM "Assignments"
                         WHERE "AssignmentId" = @AssignmentId
                         RETURNING *;
                         """;

       
        var param = new DynamicParameters();
        param.Add("AssignmentId", assignmentId);
        
        return await ExecuteQuerySingleAsync<Assignments>(sqlRequest, param, cancellationToken);
    }
}