using Hawalayk_APP.Context;
using Hawalayk_APP.Models;

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
    }
}
