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
            return await _context.Enrollment.ToListAsync();
        }

        public async Task<Enrollment> GetEnrollmentById(int id)
        {
            return await _context.Enrollment.FirstOrDefaultAsync(m => m.EnrollmentID == id);
        }

        public async Task AddEnrollment(Enrollment enrollment)
        {
            _context.Add(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEnrollment(Enrollment enrollment, int id)
        {
            try
            {
                Enrollment enrollment_old = await GetEnrollmentById(id);
                if (enrollment_old == null)
                {
                    throw new Exception("Enrollment not found");
                }

                UpdateEnrollmentProperties(enrollment_old, enrollment);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        private void UpdateEnrollmentProperties(Enrollment enrollment_old, Enrollment enrollment)
        {
            if (enrollment.CourseID != null && enrollment_old.CourseID != enrollment.CourseID)
            {
                enrollment_old.CourseID = enrollment.CourseID;
            }

            if (enrollment.StudentID != null && enrollment_old.StudentID != enrollment.StudentID)
            {
                enrollment_old.StudentID = enrollment.StudentID;
            }

            if (enrollment.Grade != null && enrollment_old.Grade != enrollment.Grade)
            {
                enrollment_old.Grade = enrollment.Grade;
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
