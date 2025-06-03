using StudyTracker.Application.Contracts.External;
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

    public Task<Assignments[]> UpdateOrInsertAssignment(Guid studentId, Guid assignmentId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}