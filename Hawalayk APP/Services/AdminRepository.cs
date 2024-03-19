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

        public Admin GetById(string id)
        {
            Admin admin = Context.Admins.FirstOrDefault(s => s.Id == id);
            return admin;
        }
        public List<Admin> GetAll()
        {
            return Context.Admins.ToList();
        }
        public int Create(Admin admin)
        {
            Context.Admins.Add(admin);
            int row = Context.SaveChanges();
            return row;
        }

        public int Delete(string id)
        {
            Admin admin = Context.Admins.FirstOrDefault(s => s.Id == id);
            Context.Admins.Remove(admin);
            int row = Context.SaveChanges();
            return row;
        }

    }
}
