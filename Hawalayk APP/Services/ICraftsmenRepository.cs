using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface ICraftsmenRepository
    {
        List<Craftsman> GetAll();
        Task<Craftsman> GetById(string id);
        Task<Post> AddPostToGallaryAsync(string craftsmanId, PostDTO post);
        Task<CraftsmanAccountDTO> GetCraftsmanAccountAsync(Craftsman craftsman);
       // Task<CraftsmanAccountDTO> UpdateCraftsmanAccountAsync(Craftsman craftssman ,CraftsmanAccountDTO craftsmanAccount);


    }
}