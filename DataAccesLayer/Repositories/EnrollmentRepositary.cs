using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Data;
using DataAccessLayer.Entity;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories
{
    public class EnrollmentRepositary : IEnrollmentRepositary
    {
        private readonly SchoolContext _context;

        public EnrollmentRepositary(SchoolContext context)
        {
            _context = context;
        }

        public async Task<List<Enrollment>> GetAllEnrollments()
        {
            var enrollments = await _context.Enrollment
            .Select(e => new Enrollment
            {
                EnrollmentID = e.EnrollmentID,
                CourseID = e.CourseID,
                StudentID = e.StudentID,
                Grade = e.Grade,
                Student = new Student
                {
                    LastName = e.Student.LastName,
                    FirstMidName = e.Student.FirstMidName,
                    JoinedDate = e.Student.JoinedDate,
                },
                Course = new Course
                {
                    Title = e.Course.Title,
                    Credits = e.Course.Credits
                }
            })
            .ToListAsync();

            return enrollments;
        }


        public async Task<Enrollment> GetEnrollmentById(int id)
        {
            var enrollmentDetails = await _context.Enrollment
            .Where(e => e.EnrollmentID == id)
        .Select(e => new Enrollment
        {
            EnrollmentID = e.EnrollmentID,
            CourseID = e.CourseID,
            StudentID = e.StudentID,
            Grade = e.Grade,
            Student = new Student
            {
                LastName = e.Student.LastName,
                FirstMidName = e.Student.FirstMidName,
                JoinedDate = e.Student.JoinedDate,
            },
            Course = new Course
            {
                Title = e.Course.Title,
                Credits = e.Course.Credits
            }
        })
        .FirstOrDefaultAsync();

            return enrollmentDetails;
        }

        public async Task AddEnrollment(Enrollment enrollment)
        {
            _context.Add(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEnrollment(Enrollment enrollment_new, int id)
        {
            try
            {
                Enrollment enrollment_old = await GetEnrollmentById(id);
                if (enrollment_old == null)
                {
                    throw new Exception("Enrollment not found");
                }

                UpdateEnrollmentProperties(enrollment_old, enrollment_new);
                _context.Entry(enrollment_old).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        private void UpdateEnrollmentProperties(Enrollment enrollment_old, Enrollment enrollment_new)
        {
            if (enrollment_new.CourseID != null && enrollment_old.CourseID != enrollment_new.CourseID)
            {
                enrollment_old.CourseID = enrollment_new.CourseID;
            }

            if (enrollment_new.StudentID != null && enrollment_old.StudentID != enrollment_new.StudentID)
            {
                enrollment_old.StudentID = enrollment_new.StudentID;
            }

            if (enrollment_new.Grade != null && enrollment_old.Grade != enrollment_new.Grade)
            {
                enrollment_old.Grade = enrollment_new.Grade;
            }
        }



        public async Task DeleteEnrollment(Enrollment enrollment)
        {
            _context.Enrollment.Remove(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<int>> GetCourseIDsAsync()
        {
            return await _context.Course.Select(course => course.CourseID).ToListAsync();
        }

        public async Task<IEnumerable<int>> GetStudentIDsAsync()
        {
            return await _context.Student.Select(student => student.ID).ToListAsync();
        }

    }
}
