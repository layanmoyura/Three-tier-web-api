using DataAccessLayer.Entity;
using DataAccessLayer.Interfaces;
using BusinessLayer.Interfaces;

namespace BusinessLayer.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepositary _enrollmentRepository;

        public EnrollmentService(IEnrollmentRepositary enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<List<Enrollment>> GetAllEnrollments()
        {
            return await _enrollmentRepository.GetAllEnrollments();
        }

        public async Task<Enrollment> GetEnrollmentById(int id)
        {
            return await _enrollmentRepository.GetEnrollmentById(id);
        }

        public async Task AddEnrollment(Enrollment enrollment)
        {
            await _enrollmentRepository.AddEnrollment(enrollment);
        }

        public async Task UpdateEnrollment(Enrollment enrollment,int id)
        {
            await _enrollmentRepository.UpdateEnrollment(enrollment,id);
        }

        public async Task DeleteEnrollment(int id)
        {
            await _enrollmentRepository.DeleteEnrollment(id);
        }

       
    }
}
