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
}