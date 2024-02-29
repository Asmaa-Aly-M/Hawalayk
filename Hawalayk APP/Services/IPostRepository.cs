using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IPostRepository
    {
        Task<List<PostDTO>> GetCraftGallary(int craftId);


        Task<Post> CreatNewPostAsync(Craftsman craftsman, PostDTO newPost);
        int Delete(int id);
        int Update(int id, Post newPost);
        List<Post> GetAll();
        Post GetById(int id);
       
    }
}