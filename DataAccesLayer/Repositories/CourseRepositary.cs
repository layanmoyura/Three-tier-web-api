using DataAccessLayer.Data;
using DataAccessLayer.Entity;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

public class CourseRepositary : ICourseRepositary
{
    private readonly SchoolContext _context;

    public CourseRepositary(SchoolContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetCoursesAsync()
    {
        var courseList = await _context.Course
     .Select(c => new Course
     {
         CourseID = c.CourseID,
         Title = c.Title,
         Credits = c.Credits,
         Enrollments = c.Enrollments.Select(e => new Enrollment
         {
             EnrollmentID = e.EnrollmentID,
             StudentID = e.StudentID,
             Grade = e.Grade
         }).ToList()
     })
     .ToListAsync();

        return courseList;
    }

    public async Task<Course> GetCourseByIdAsync(int id)
    {
        var courseDetails = await _context.Course
      .Where(c => c.CourseID == id)
      .Select(c => new Course
      {
          CourseID = c.CourseID,
          Title = c.Title,
          Credits = c.Credits,
          Enrollments = c.Enrollments.Select(e => new Enrollment
          {
              EnrollmentID = e.EnrollmentID,
              StudentID = e.StudentID,
              Grade = e.Grade
          }).ToList()
      })
      .FirstOrDefaultAsync();

        return courseDetails;
    }

    public async Task AddCourseAsync(Course course)
    {
        _context.Add(course);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCourseAsync(Course course_new, int id)
    {
        try
        {
            Course course_old = await GetCourseByIdAsync(id);
            if (course_old == null)
            {
                throw new Exception("Course not found");
            }

            UpdateCourseProperties(course_old, course_new);
            _context.Entry(course_old).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }

    private void UpdateCourseProperties(Course course_old, Course course_new)
    {
        if (!string.IsNullOrEmpty(course_new.Title) && course_old.Title != course_new.Title)
        {
            course_old.Title = course_new.Title;
        }

        if (course_new.Credits != null && course_old.Credits != course_new.Credits)
        {
            course_old.Credits = course_new.Credits;
        }
    }


    public async Task DeleteCourseAsync(int id)
    {
        Course course = _context.Course.Find(id);
        _context.Course.Remove(course);
        await _context.SaveChangesAsync();
    }

    
}
