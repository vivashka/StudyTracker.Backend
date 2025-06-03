using StudyTracker.Application.Contracts;
using StudyTracker.Application.Contracts.External;
using StudyTracker.Application.Models;
using StudyTracker.Domain.Models;

namespace StudyTracker.Application.Services;

public class AssignmentService(IAssignmentProvider assignmentProvider) : IAssignmentService
{
    public async Task<ResponseModel<Assignments[]>> GetAssignments(Guid studentId, Guid courseId)
    {
        try
        {
            var assignments = await assignmentProvider.GetAssignments(studentId, courseId, CancellationToken.None);

            if (assignments.Length > 0)
            {
                return new ResponseModel<Assignments[]>(assignments,
                    true,
                    null);
            }
            return new ResponseModel<Assignments[]>(assignments,
                true,
                new ActionErrorModel(200, "Курсов не найдено"));
        }
        catch (Exception ex)
        {
            return new ResponseModel<Assignments[]>([],
                false,
                new ActionErrorModel(400, "Непредвиденная ошибка " + ex.Message));
        }
        
    }

    public Task<Assignments[]> UpdateOrInsertAssignment(Guid studentId, Guid assignmentId)
    {
        throw new NotImplementedException();
    }
}