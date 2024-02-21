using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IAdvertisementRepository
    {
        int Create(Advertisement Advertis);
        int Delete(int id);
        List<Advertisement> GetAll();
        Advertisement GetById(int id);
        int Update(int id, Advertisement Advertis);
    }
}