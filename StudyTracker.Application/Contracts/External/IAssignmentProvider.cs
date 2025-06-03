using StudyTracker.Domain.Models;

namespace StudyTracker.Application.Contracts.External;

public interface IAssignmentProvider
{
    public Task<Assignments[]> GetAssignments(Guid studentId, Guid courseId, CancellationToken cancellationToken);
    
    public Task<Assignments[]> UpdateOrInsertAssignment(Guid studentId, Guid assignmentId, CancellationToken cancellationToken);
}