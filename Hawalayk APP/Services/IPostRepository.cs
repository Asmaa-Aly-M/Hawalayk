using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IPostRepository
    {
        Task<int> Create(string craftsmanId, PostDTO postDTO);
        Task<int> Delete(int id);
        Task<List<Post>> GetAll();
        Task<Post> GetById(int id);
        Task<List<GallaryPostDTO>> GetGrafGallary(string craftName);
        Task<List<GallaryPostDTO>> GetGraftsmanPortfolio(string craftsmanId);
        Task<int> Update(int id, Post newPost);
    }
}