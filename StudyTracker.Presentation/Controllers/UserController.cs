using Microsoft.AspNetCore.Mvc;
using StudyTracker.Application.Contracts;
using StudyTracker.Presentation.Models;

namespace StudyTracker.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IStudentService studentService) : ControllerBase
{
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthModel user)
    {
        var response = await studentService.Authenticate(user.Login, user.Password);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Registration(AuthModel user)
    {
        var response = await studentService.Registration(user.Login, user.Password);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetUsers()
    {
        var response = await studentService.GetUsers();
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
    
    [HttpGet("byCourses")]
    public async Task<IActionResult> GetUsersByCourses(Guid courseId)
    {
        var response = await studentService.GetUsersByCourse(courseId);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}