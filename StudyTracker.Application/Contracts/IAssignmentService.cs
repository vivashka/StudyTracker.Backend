using StudyTracker.Application.Models;
using StudyTracker.Domain.Enums;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.Contracts;

public interface IAssignmentService
{
    public Task<ResponseModel<Assignments[]>> GetAssignments(Guid studentId, Guid courseId);
    
    public Task<ResponseModel<Assignments>> UpdateOrInsertAssignment(Guid? studentId, Assignments assignment);
    
    public Task<ResponseModel<int>> UpdateOrInsertState(Guid assignmentId, Guid studentId, TaskState state);
    
    public Task<ResponseModel<bool>> DeleteAssignment(Guid assignmentId);
}