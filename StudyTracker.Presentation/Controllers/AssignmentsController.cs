using Microsoft.AspNetCore.Mvc;
using StudyTracker.Application.Contracts;
using StudyTracker.Domain.Enums;
using StudyTracker.Presentation.Models.RequestModels;

namespace StudyTracker.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssignmentsController(IAssignmentService assignmentService) : ControllerBase
{
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAssignments(Guid studentId, Guid courseId)
    {
        var response = await assignmentService.GetAssignments(studentId, courseId);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> UpdateOrInsertAssignment([FromBody] UpdateAssignmentRequest updateAssignmentRequest)
    {
        var response = await assignmentService.UpdateOrInsertAssignment(updateAssignmentRequest.StudentId, updateAssignmentRequest.Assignments);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
    [HttpPut]
    [Route("State")]
    public async Task<IActionResult> UpdateOrInsertState(Guid assignmentId, Guid studentId, TaskState state)
    {
        var response = await assignmentService.UpdateOrInsertState(assignmentId, studentId, state);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}