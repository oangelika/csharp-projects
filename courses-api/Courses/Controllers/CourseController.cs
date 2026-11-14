namespace Courses.Controllers;

using Courses.DTOs;
using Courses.Services;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/courses")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _service;
    public CoursesController(ICourseService service) => _service = service;

    [HttpPost("with-enrollments")]
    public async Task<IActionResult> CreateCourseWithStudents([FromBody] CreateCourseRequestDto dto)
    {
        var result = await _service.CreateCourseWithEnrollmentsAsync(dto);
        return Ok(result);
    }
}
