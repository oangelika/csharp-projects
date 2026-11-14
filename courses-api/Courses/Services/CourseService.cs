namespace Courses.Services;

using Courses.Data;
using Courses.DTOs;
using Courses.Models;
using Microsoft.EntityFrameworkCore;


public interface ICourseService
{
    Task<List<EnrollmentResponseDto>> GetAllEnrollmentsAsync();
    Task<object> CreateCourseWithEnrollmentsAsync(CreateCourseRequestDto request);
}

public class CourseService : ICourseService
{
    private readonly AppDbContext _context;
    public CourseService(AppDbContext context) => _context = context;

    public async Task<List<EnrollmentResponseDto>> GetAllEnrollmentsAsync()
    {
        return await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .Select(e => new EnrollmentResponseDto
            {
                Student = new {
                    e.Student.Id,
                    e.Student.FirstName,
                    e.Student.LastName,
                    e.Student.Email
                },
                Course = new {
                    e.Course.Id,
                    e.Course.Title,
                    e.Course.Teacher
                },
                EnrollmentDate = e.EnrollmentDate
            })
            .ToListAsync();
    }

    public async Task<object> CreateCourseWithEnrollmentsAsync(CreateCourseRequestDto request)
    {
        var course = new Course
        {
            Title = request.Title,
            Credits = request.Credits,
            Teacher = request.Teacher
        };
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        var now = DateTime.Now;
        var enrollments = new List<object>();

        foreach (var s in request.Students)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(st => st.FirstName == s.FirstName &&
                                           st.LastName == s.LastName &&
                                           st.Email == s.Email);

            if (student == null)
            {
                student = new Student
                {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email
                };
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
            }

            _context.Enrollments.Add(new Enrollment
            {
                StudentId = student.Id,
                CourseId = course.Id,
                EnrollmentDate = now
            });

            enrollments.Add(new {
                studentId = student.Id,
                student.FirstName,
                student.LastName,
                student.Email,
                enrollmentDate = now
            });
        }

        await _context.SaveChangesAsync();

        return new
        {
            message = "Kurs został utworzony i studenci zostali zapisani.",
            course = new {
                course.Id,
                course.Title,
                course.Credits,
                course.Teacher
            },
            enrollments
        };
    }
}

