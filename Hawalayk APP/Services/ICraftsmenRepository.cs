using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface ICraftsmenRepository
    {
        List<Craftsman> GetAll();
        Craftsman GetById(string id);
        Task<CraftsmanAccountDTO> GetCraftsmanAccountAsync(Craftsman craftsman);
        Task<UpdateUserDTO> UpdateCraftsmanAccountAsync(string craftsmanId, CraftsmanUpdatedAccountDTO craftsmanAccount);
        Task<List<Craftsman>> GetPendingCraftsmen();
        Task<Craftsman> ApproveCraftsman(string id, bool isApproved);

        int craftsmanNumber();

    }
}