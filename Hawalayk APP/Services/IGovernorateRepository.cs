using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IGovernorateRepository
    {
        Task<List<Governorate>> GetAllAsync();
        Task<Governorate> GetByIdAsync(int id);
        Task<Governorate> GetByNameAsync(string govName);
        Task<List<string>> GetGovernorateNamesAsync();
    }
}
