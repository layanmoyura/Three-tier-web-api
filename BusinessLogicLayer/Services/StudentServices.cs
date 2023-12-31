using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entity;


namespace BusinessLayer.Services
{
    public class StudentServices : IStudentServices
    {
        private readonly IStudentRepository _studentRepository;

        public StudentServices(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            var students = await _studentRepository.GetStudentsAsync();


            return students;
        }

        public async Task<Student> GetStudentDetailsAsync(int id)
        {
            return await _studentRepository.GetDetStudentByIdAsync(id);           
        }

        public async Task CreateStudentAsync(Student student)
        {
             await _studentRepository.InsertStudentAsync(student);
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _studentRepository.GetStudentById(id);
        }

        public async Task UpdateStudentAsync(Student student, int id)
        {
            await _studentRepository.UpdateStudentAsync(student,id);
        }
        public async Task DeleteStudentAsync(int id)
        {
            await _studentRepository.DeleteStudentAsync(id);
        }

        

    }
}
