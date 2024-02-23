using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface ICraftsmenRepository
    {
        List<Craftsman> GetAll();
        Craftsman GetById(string id);
    }
}