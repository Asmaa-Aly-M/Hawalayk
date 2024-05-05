using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IAdvertisementRepository
    {
        Task<int> Create(Advertisement Advertis);
        Task<int> Delete(int id);
        Task<List<Advertisement>> GetAll();
        Task<Advertisement> GetById(int id);
        Task<int> Update(int id, Advertisement Advertis);
    }
}