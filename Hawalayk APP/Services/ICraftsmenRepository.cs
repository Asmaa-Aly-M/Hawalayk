using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface ICraftsmenRepository
    {
        List<Craftsmen> GetAll();
        Craftsmen GetById(string id);
    }
}