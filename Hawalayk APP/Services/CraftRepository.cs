using Hawalayk_APP.Context;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public class CraftRepository : ICraftRepository
    {
        ApplicationDbContext Context;
        public CraftRepository(ApplicationDbContext _Context)
        {
            Context = _Context;
        }




        public Craft GetById(int id)
        {
            Craft craft = Context.Crafts.FirstOrDefault(s => s.Id == id);
            return craft;
        }
        public List<Craft> GetAll()
        {
            return Context.Crafts.ToList();
        }

        public int Create(Craft newCraft)
        {
            Context.Crafts.Add(newCraft);
            int row = Context.SaveChanges();
            return row;
        }
        public int Update(int id, Craft newCraft)
        {
            Craft OldCraft = Context.Crafts.FirstOrDefault(s => s.Id == id);
            OldCraft.Name = newCraft.Name;

            int row = Context.SaveChanges();
            return row;
        }
        public int Delete(int id)
        {
            Craft OldCraft = Context.Crafts.FirstOrDefault(s => s.Id == id);
            Context.Crafts.Remove(OldCraft);
            int row = Context.SaveChanges();
            return row;
        }
    }
}
