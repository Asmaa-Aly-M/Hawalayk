using Hawalayk_APP.Context;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Services
{
    
    public class AdminRepository : IAdminRepository
    {
        ApplicationDbContext Context;
        public AdminRepository(ApplicationDbContext _Context)
        {
            Context = _Context;
        }

        public async Task<Admin> GetById(string id)
        {
            Admin admin = await Context.Admins.FirstOrDefaultAsync(s => s.Id == id);
            return admin;
        }
        public async Task<List<Admin>> GetAll()
        {
            return await Context.Admins.ToListAsync();
        }
        public async Task<int> Create(Admin admin)
        {
            Context.Admins.Add(admin);
            int row = await Context.SaveChangesAsync();
            return row;
        }

        public async Task<int> Delete(string id)
        {
            Admin admin = await Context.Admins.FirstOrDefaultAsync(s => s.Id == id);
            Context.Admins.Remove(admin);
            int row = await Context.SaveChangesAsync();
            return row;
        }

    }
}
