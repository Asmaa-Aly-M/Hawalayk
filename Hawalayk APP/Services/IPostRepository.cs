using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface IPostRepository
    {
        int Create(Post newPost);
        int Delete(int id);
        List<Post> GetAll();
        Post GetById(int id);
       
    }
}