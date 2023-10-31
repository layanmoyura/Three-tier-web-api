using DataAccessLayer.Entity;

namespace DataAccessLayer.Interfaces
{
    public interface IStudentRepository 
    {
        Task <IEnumerable<Student>> GetStudentsAsync();
        Task<Student> GetDetStudentByIdAsync(int id);
        Task<Student> GetStudentById(int id);


        Task InsertStudentAsync(Student student);

        Task UpdateStudentAsync(Student student, int id);

        Task DeleteStudentAsync(int id);

       
        
    }
}