using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;

namespace Hawalayk_APP.Services
{
    public class CraftRepository : ICraftRepository
    {
        ApplicationDbContext Context;
        private readonly IBlockingRepository _blockingRepository;
        public CraftRepository(ApplicationDbContext _Context, IBlockingRepository blockingRepository)
        {
            Context = _Context;
            _blockingRepository = blockingRepository;
        }


        public async Task<Craft> GetOrCreateCraftAsync(string craftName)
        {

            //CraftName craft_Name;
            Craft existingCraft = null;
            CraftName enumValue = await GetEnumValueOfACraftByArabicDesCription(craftName);

            //if (Enum.TryParse(craftName, out craft_Name))
            //{
            existingCraft = await Context.Crafts.FirstOrDefaultAsync(c => c.Name == enumValue);

            //}//parsing : enum 

            if (existingCraft != null)
            {
                return existingCraft;
            }

            var newCraft = new Craft { Name = enumValue };
            Context.Crafts.Add(newCraft);
            await Context.SaveChangesAsync();

            return newCraft;
        }

        public async Task<List<Craft>> GetAll()
        {
            return await Context.Crafts.ToListAsync();
        }

        public Task<List<string>> GetAllCraftsNamesAsync()
        {
            var craftNames = Enum.GetNames(typeof(CraftName)).ToList();
            return Task.FromResult(craftNames);
        }
        public async Task<List<CraftsmanDTO>> GetCraftsmenOfACraft(string userId, string craftName)
        {
            Craft existingCraft = null;
            CraftName enumValue = await GetEnumValueOfACraftByArabicDesCription(craftName);

            var blockedUserIds = await _blockingRepository.GetBlockedUsersAsync(userId);

            var craftsmen = await Context.Craftsmen.Include(c => c.Craft).Where(c => c.Craft.Name == enumValue).ToListAsync();

            var filteredCraftsmen = craftsmen.Where(craftsman => !blockedUserIds.Contains(craftsman.Id)).ToList();

            return filteredCraftsmen.Select(c => new CraftsmanDTO
            {
                CraftName = craftName,
                UserName = c.UserName,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Rating = c.Rating,
                Id = c.Id


            }).ToList();

        }








        public async Task<Craft> GetById(int id)
        {
            Craft craft = await Context.Crafts.FirstOrDefaultAsync(s => s.Id == id);
            return craft;
        }

        public async Task<int> GetCraftIdByCraftEnumValue(CraftName value)
        {
            var craft = await Context.Crafts.FirstOrDefaultAsync(c => c.Name == value);
            return craft.Id;
        }

        public async Task<int> Create(Craft newCraft)
        {
            Context.Crafts.Add(newCraft);
            int row = await Context.SaveChangesAsync();
            return row;
        }
        public async Task<int> Update(int id, Craft newCraft)
        {
            Craft OldCraft = await Context.Crafts.FirstOrDefaultAsync(s => s.Id == id);
            OldCraft.Name = newCraft.Name;

            int row = await Context.SaveChangesAsync();
            return row;
        }
        public async Task<int> Delete(int id)
        {
            Craft OldCraft = await Context.Crafts.FirstOrDefaultAsync(s => s.Id == id);
            Context.Crafts.Remove(OldCraft);
            int row = await Context.SaveChangesAsync();
            return row;
        }

        //public async Task<string>GetCraftName(int id)
        //{
        //    var craft = Context.Crafts.FirstOrDefaultAsync(c=>c.Id==id);

        //}
        public async Task<string> GetCraftNameInArabicByEnumValue(CraftName enumValue)
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            DescriptionAttribute attribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();
            return attribute.Description;

        }
        public async Task<CraftName> GetEnumValueOfACraftByArabicDesCription(string craftArabicDes)
        {
            CraftName enumValue = (CraftName)ConvertToEnum<CraftName>(craftArabicDes);
            return enumValue;

        }
        private static T? ConvertToEnum<T>(string arabicString) where T : struct
        {
            Type enumType = typeof(T);

            if (enumType.IsEnum)
            {
                foreach (FieldInfo field in enumType.GetFields())
                {
                    if (Attribute.GetCustomAttribute(field,
                        typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                    {
                        if (attribute.Description == arabicString)
                        {
                            return (T)field.GetValue(null);
                        }
                    }
                }
            }
            return null;
        }
    }
}
