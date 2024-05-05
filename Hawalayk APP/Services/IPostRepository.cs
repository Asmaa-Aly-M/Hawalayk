using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IPostRepository
    {
        Task<int> Create(string craftsmanId, PostDTO postDTO);
        int Delete(int id);
        List<Post> GetAll();
        Post GetById(int id);
        List<GallaryPostDTO> GetGrafGallary(string craftName);
        Task<List<GallaryPostDTO>> GetGraftsmanPortfolio(string craftsmanId);
        int Update(int id, Post newPost);
    }
}