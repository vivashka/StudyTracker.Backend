using StudyTracker.Application.Contracts;
using StudyTracker.Application.Contracts.External;
using StudyTracker.Application.Models;
using StudyTracker.Domain.Enums;
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
                new ActionErrorModel(200, "Заданий не найдено"));
        }
        catch (Exception ex)
        {
            return new ResponseModel<Assignments[]>([],
                false,
                new ActionErrorModel(400, "Непредвиденная ошибка " + ex.Message));
        }
        
    }

    public async Task<ResponseModel<Assignments>> UpdateOrInsertAssignment(Guid? studentId, Assignments assignment)
    {
        try
        {
            var newAssignments = await assignmentProvider.UpdateOrInsertAssignment(assignment, CancellationToken.None);

            if (studentId != null)
            {
                var taskState = UpdateOrInsertState((Guid)(assignment.AssignmentId ?? newAssignments.AssignmentId)!, (Guid)studentId,
                    TaskState.NotStarted);
            }
            

            if (newAssignments.AssignmentId != null)
            {
                return new ResponseModel<Assignments>(newAssignments,
                    true,
                    null);
            }
            return new ResponseModel<Assignments>(newAssignments,
                true,
                new ActionErrorModel(200, "Не удалось добавить задание"));
        }
        catch (Exception ex)
        {
            return new ResponseModel<Assignments>(new Assignments(),
                false,
                new ActionErrorModel(400, "Непредвиденная ошибка " + ex.Message));
        }
    }

    public async Task<ResponseModel<int>> UpdateOrInsertState(Guid assignmentId, Guid studentId, TaskState state)
    {
        try
        {
            var taskState = await assignmentProvider.UpdateOrInsertState(assignmentId, studentId, state, CancellationToken.None);

            if (taskState == (int)state)
            {
                return new ResponseModel<int>(taskState,
                    true,
                    null);
            }
            return new ResponseModel<int>(-1,
                true,
                new ActionErrorModel(200, "Не удалось обновить состояние задания"));
        }
        catch (Exception ex)
        {
            return new ResponseModel<int>(-1,
                false,
                new ActionErrorModel(400, "Непредвиденная ошибка " + ex.Message));
        }
    }

    public async Task<ResponseModel<bool>> DeleteAssignment(Guid assignmentId)
    {
        try
        {
            var assignments = await assignmentProvider.DeleteAssignment(assignmentId, CancellationToken.None);

            if (assignments.AssignmentId != null)
            {
                return new ResponseModel<bool>(true,
                    true,
                    null);
                
            }
            return new ResponseModel<bool>(false,
                true,
                new ActionErrorModel(200, "Не удалось удалить задание"));
        }
        catch (Exception ex)
        {
            return new ResponseModel<bool>(false,
                false,
                new ActionErrorModel(400, "Непредвиденная ошибка " + ex.Message));
        }
    }
}