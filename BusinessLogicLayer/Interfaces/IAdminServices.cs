using DataAccessLayer.Entity;

namespace BusinessLayer.Interfaces
{
    public interface IAdminServices
    {
        Task AddAdminAsync(Admin admin);

        Task<Admin> GetAdminByEmailAsync(string EmailAddress);

    }
}
