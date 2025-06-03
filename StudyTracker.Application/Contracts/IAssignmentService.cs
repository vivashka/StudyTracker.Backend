using StudyTracker.Application.Models;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.Contracts;

public interface IAssignmentService
{
    public Task<ResponseModel<Assignments[]>> GetAssignments(Guid studentId, Guid courseId);
    
    public Task<Assignments[]> UpdateOrInsertAssignment(Guid studentId, Guid assignmentId);
}