using DataAccessLayer.Entity;

namespace BusinessLayer.Interfaces
{
    public interface IStudentServices
    {
        Task<IEnumerable<Student>> GetStudentsAsync(string sortOrder, string searchString);
        Task<Student> GetStudentDetailsAsync(int id);
        Task CreateStudentAsync(Student student);
        Task<Student> GetStudentById(int id);
        Task UpdateStudentAsync(Student student, int id);
        Task DeleteStudentAsync(int id);

    }
}
