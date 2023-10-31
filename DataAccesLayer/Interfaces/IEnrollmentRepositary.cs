using DataAccessLayer.Entity;

namespace DataAccessLayer.Interfaces
{
    public interface IEnrollmentRepositary
    {
        Task<List<Enrollment>> GetAllEnrollments();
        Task<Enrollment> GetEnrollmentById(int id);
        Task AddEnrollment(Enrollment enrollment);

        Task UpdateEnrollment(Enrollment enrollment,int id);
        Task DeleteEnrollment(Enrollment enrollment);
        Task<IEnumerable<int>> GetCourseIDsAsync();
        Task<IEnumerable<int>> GetStudentIDsAsync();


    } 
}
