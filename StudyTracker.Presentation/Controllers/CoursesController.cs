using Microsoft.AspNetCore.Mvc;
using StudyTracker.Application.Contracts;
using StudyTracker.Presentation.Models.RequestModels;

namespace StudyTracker.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController(ICoursesService coursesService) : ControllerBase
{
    [HttpGet("{studentId:guid}")]
    public async Task<IActionResult> GetCourses(Guid studentId)
    {
        var response = await coursesService.GetCourses(studentId);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    } 
    
    [HttpPost("Create")]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest createCourseRequest)
    {
        var response = await coursesService.CreateCourse(createCourseRequest.Course, createCourseRequest.StudentId);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    } 
    
    [HttpPut("Assign")]
    public async Task<IActionResult> AssignCourse(Guid courseId, Guid studentId)
    {
        var response = await coursesService.AssignCourse(courseId, studentId);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    } 
    
    [HttpDelete("")]
    public async Task<IActionResult> DeleteCourse(Guid courseId, Guid studentId)
    {
        var response = await coursesService.DeleteCourse(courseId, studentId);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    } 
}