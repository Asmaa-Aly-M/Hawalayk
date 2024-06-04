using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface ICityRepository
    {
        Task<List<City>> GetAllAsync();

        Task<City> GetByIdAsync(int id);

        Task<City> GetByNameAsync(string cityName);

        Task<List<string>> GetCitiesNamesByGovernorateNameAsync(string govName);
    }
}
