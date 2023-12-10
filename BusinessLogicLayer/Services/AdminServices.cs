using BusinessLayer.Interfaces;
using DataAccessLayer.Entity;
using DataAccessLayer.Interfaces;


namespace BusinessLayer.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly IAdminRepositary _repository;

        public AdminServices(IAdminRepositary repository)
        {
            _repository = repository;
        }

        public async Task AddAdminAsync(Admin admin)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(admin.Password);
            admin.Password = hashedPassword;
            await _repository.AddAdminAsync(admin);
        }

        public async Task<Admin> GetAdminByEmailAsync(string EmailAddress)
        {
            return await _repository.GetAdminByEmailAsync(EmailAddress);
        }


    }
}
