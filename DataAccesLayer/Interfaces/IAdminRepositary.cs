using DataAccessLayer.Entity;


namespace DataAccessLayer.Interfaces
{
    public interface IAdminRepositary
    {
        Task AddAdminAsync(Admin admin);
        Task<Admin> GetAdminByEmailAsync(string EmailAddress);
    }
}
