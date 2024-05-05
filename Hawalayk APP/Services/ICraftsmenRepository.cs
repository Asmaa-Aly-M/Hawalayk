using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface ICraftsmenRepository
    {
<<<<<<< HEAD
        List<Craftsman> GetAll();
=======
        Task<List<Craftsman>> GetAll();
>>>>>>> d44a62dc11f45570f618f209d5262876bbd75df0
        Task<Craftsman> GetById(string id);
        Task<CraftsmanAccountDTO> GetCraftsmanAccountAsync(Craftsman craftsman);
        Task<UpdateUserDTO> UpdateCraftsmanAccountAsync(string craftsmanId, CraftsmanUpdatedAccountDTO craftsmanAccount);
        Task<List<Craftsman>> GetPendingCraftsmen();
        Task<Craftsman> ApproveCraftsman(string id, bool isApproved);

        Task<int> craftsmanNumber();

    }
}