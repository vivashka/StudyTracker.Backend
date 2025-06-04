using StudyTracker.Domain.Enums;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.Contracts.External;

public interface IAssignmentProvider
{
    public Task<Assignments[]> GetAssignments(Guid studentId, Guid courseId, CancellationToken cancellationToken);
    
    public Task<Assignments> UpdateOrInsertAssignment(Assignments assignment, CancellationToken cancellationToken);
    
    public Task<int> UpdateOrInsertState(Guid assignmentId, Guid studentId, TaskState state, CancellationToken cancellationToken);
    
     public Task<Assignments> DeleteAssignment(Guid assignmentId, CancellationToken cancellationToken);
}