using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hawalayk_APP.Services
{
    public class CraftRepository : ICraftRepository
    {
        ApplicationDbContext Context;
        public CraftRepository(ApplicationDbContext _Context)
        {
            Context = _Context;
        }

        public async Task<Craft> GetOrCreateCraftAsync(string  craftName)
        {

            CraftName craft_Name;
            Craft existingCraft = null;
            if (Enum.TryParse(craftName, out craft_Name))
            {
                existingCraft = await Context.Crafts.FirstOrDefaultAsync(c => c.Name == craft_Name);

            }//parsing : enum 

            if (existingCraft != null)
            {
                return existingCraft;
            }

            var newCraft = new Craft { Name = craft_Name };
            Context.Crafts.Add(newCraft);
            await Context.SaveChangesAsync();

            return newCraft;
        }


        public List<Craft> GetAll()
        {
            return Context.Crafts.ToList();
        }
        public Task<List<string>> GetAllCraftsNamesAsync()
        {
            var craftNames = Enum.GetNames(typeof(CraftName)).ToList();
            return Task.FromResult(craftNames);
        }
        public async Task<List<CraftsmanDTO>> GetCraftsmenOfACraft(CraftName craftName)
        {
            var craftsmen= await Context.Craftsmen.Include(c => c.Craft).Where(c => c.Craft.Name == craftName).ToListAsync();
            return craftsmen.Select(c => new CraftsmanDTO
            {
                CraftName = Enum.GetName(typeof(CraftName), craftName),
                UserName =c.UserName,
                FirstName=c.FirstName,
                LastName=c.LastName,
                Rating=c.Rating,


            }).ToList();

        }









        public Craft GetById(int id)
        {
            Craft craft = Context.Crafts.FirstOrDefault(s => s.Id == id);
            return craft;
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
