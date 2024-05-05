using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IAdminRepository
    {
        Task<List<Admin>> GetAll();
        Task<Admin> GetById(string id);
        Task<int> Create(Admin admin);
        Task<int> Delete(string id);
    }
}