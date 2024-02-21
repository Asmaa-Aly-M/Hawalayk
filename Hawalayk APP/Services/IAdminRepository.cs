using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IAdminRepository
    {
        List<Admin> GetAll();
        Admin GetById(string id);
    }
}