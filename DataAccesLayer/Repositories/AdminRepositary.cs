using DataAccessLayer.Data;
using DataAccessLayer.Entity;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class AdminRepositary : IAdminRepositary
    {
        private readonly SchoolContext _context;

        public AdminRepositary(SchoolContext context)
        {
            _context = context;
        }
        public async Task AddAdminAsync(Admin admin)
        {
            _context.Add(admin);
            await _context.SaveChangesAsync();
        }

        public async Task<Admin> GetAdminByEmailAsync(string EmailAddress)
        {
            return await _context.Admin.FirstOrDefaultAsync(a => a.EmailAddress == EmailAddress);
        }
    }
}
