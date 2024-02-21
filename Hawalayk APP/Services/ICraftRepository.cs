using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface ICraftRepository
    {
        int Create(Craft newCraft);
        int Delete(int id);
        List<Craft> GetAll();
        Craft GetById(int id);
        int Update(int id, Craft newCraft);
    }
}