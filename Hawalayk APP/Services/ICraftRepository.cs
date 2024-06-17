using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface ICraftRepository
    {
        Task<List<string>> GetAllCraftsNamesAsync();
        Task<Craft> GetOrCreateCraftAsync(string craftName);
        Task<List<CraftsmanDTO>> GetCraftsmenOfACraft(string userId, string craftName);
        Task<int> GetCraftIdByCraftEnumValue(CraftName value);
        Task<int> Create(Craft newCraft);
        Task<int> Delete(int id);
        Task<List<Craft>> GetAll();
        Task<Craft> GetById(int id);
        Task<int> Update(int id, Craft newCraft);
        Task<CraftName> GetEnumValueOfACraftByArabicDesCription(string craftArabicDes);
        Task<string> GetCraftNameInArabicByEnumValue(CraftName enumValue);
    }
}