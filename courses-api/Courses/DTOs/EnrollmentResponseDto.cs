namespace Courses.DTOs;

public class EnrollmentResponseDto
{
    public object Student { get; set; } = null!;
    public object Course { get; set; } = null!;
    public DateTime EnrollmentDate { get; set; }
}
