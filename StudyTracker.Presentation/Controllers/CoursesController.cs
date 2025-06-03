using Microsoft.AspNetCore.Mvc;
using StudyTracker.Application.Contracts;

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
}