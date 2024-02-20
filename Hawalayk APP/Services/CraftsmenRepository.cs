using Hawalayk_APP.Context;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public class CraftsmenRepository : ICraftsmenRepository
    {

        ApplicationDbContext Context;
        public CraftsmenRepository(ApplicationDbContext _Context)
        {
            Context = _Context;
        }




        public Craftsmen GetById(string id)
        {
            Craftsmen Craftman = Context.Craftsmen.FirstOrDefault(s => s.Id == id);
            return Craftman;
        }
        public List<Craftsmen> GetAll()
        {
            return Context.Craftsmen.ToList();
        }






    }
}
