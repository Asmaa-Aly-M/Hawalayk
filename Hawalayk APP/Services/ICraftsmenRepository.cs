using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface ICraftsmenRepository
    {

        Task<List<Craftsman>> GetAll();
        Task<Craftsman> GetById(string id);
        Task<CraftsmanAccountDTO> GetCraftsmanAccountAsync(Craftsman craftsman);
        Task<UpdateUserDTO> UpdateCraftsmanAccountAsync(string craftsmanId, CraftsmanUpdatedAccountDTO craftsmanAccount);
        Task<List<Craftsman>> GetPendingCraftsmen();
        Task<Craftsman> ApproveCraftsman(string id, bool isApproved);

        Task<int> craftsmanNumber();
        Task<List<GallaryPostDTO>> FilterMyCraftGallary(string craftsmanId);

        List<ServiceRequest> GetServiceRequestsByCraftName(CraftName craft);
    }
}