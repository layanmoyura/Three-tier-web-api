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
