namespace Courses.Controllers;

using Courses.Services;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/enrollments")]
public class EnrollmentsController : ControllerBase
{
    private readonly ICourseService _service;
    public EnrollmentsController(ICourseService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetEnrollments()
        => Ok(await _service.GetAllEnrollmentsAsync());
}
