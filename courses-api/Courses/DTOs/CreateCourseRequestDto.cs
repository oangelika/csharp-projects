namespace Courses.DTOs;

public class CreateCourseRequestDto
{
    public string Title { get; set; } = null!;
    public string Credits { get; set; } = null!;
    public string Teacher { get; set; } = null!;
    public List<CreateStudentDto> Students { get; set; } = new();
}

public class CreateStudentDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
}
