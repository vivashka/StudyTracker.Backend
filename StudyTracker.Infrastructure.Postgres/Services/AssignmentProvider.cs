using StudyTracker.Application.Contracts.External;
using StudyTracker.Domain.Enums;
using StudyTracker.Domain.Models;
using StudyTracker.Infrastructure.Postgres.Contracts;

namespace StudyTracker.Infrastructure.Postgres.Services;

public class AssignmentProvider(IAssignmentsRepository assignmentsRepository) : IAssignmentProvider
{
    public async Task<Assignments[]> GetAssignments(Guid studentId, Guid courseId, CancellationToken cancellationToken)
    {
        var assignments = await assignmentsRepository.GetAssignments(studentId, courseId, cancellationToken);

        return assignments;
    }

    public async Task<Assignments> UpdateOrInsertAssignment(Assignments assignment, CancellationToken cancellationToken)
    {
        var assignments = await assignmentsRepository.UpdateOrInsertAssignment(assignment, cancellationToken);

        return assignments;
    }

    public async Task<int> UpdateOrInsertState(Guid assignmentId, Guid studentId, TaskState state, CancellationToken cancellationToken)
    {
        var assignments = await assignmentsRepository.UpdateOrInsertState(assignmentId, studentId, state, cancellationToken);

        return assignments;
    }

    public async Task<Assignments> DeleteAssignment(Guid assignmentId, CancellationToken cancellationToken)
    {
        var assignments = await assignmentsRepository.DeleteAssignment(assignmentId, cancellationToken);

        return assignments;
    }
}