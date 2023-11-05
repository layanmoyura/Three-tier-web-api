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

        public async Task<IEnumerable<Student>> GetStudentsAsync(string sortOrder, string searchString)
        {
            var students = await _studentRepository.GetStudentsAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName).ToList();
                    break;
                case "Date":
                    students = students.OrderBy(s => s.JoinedDate).ToList();
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.JoinedDate).ToList();
                    break;
                default:
                    students = students.OrderBy(s => s.LastName).ToList();
                    break;
            }

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
