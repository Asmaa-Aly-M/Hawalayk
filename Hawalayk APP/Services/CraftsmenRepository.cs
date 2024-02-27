using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Services
{
    public class CraftsmenRepository : ICraftsmenRepository
    {

        ApplicationDbContext Context;
        private readonly IPostRepository _postRepository;
        public CraftsmenRepository(ApplicationDbContext _Context, IPostRepository postRepository)
        {
            Context = _Context;
            _postRepository = postRepository;
        }




        public async Task<Craftsman> GetById(string id)
        {
            Craftsman Craftman = await Context.Craftsmen.FirstOrDefaultAsync(s => s.Id == id);
            return Craftman;
        }
        public List<Craftsman> GetAll()
        {
            return Context.Craftsmen.ToList();
        }
        public async Task<Post> AddPostToGallaryAsync(string craftsmanId,PostDTO post)
        {
            var craftsman = await GetById(craftsmanId);
            return await _postRepository.CreatNewPostAsync(craftsman,post);

        }

        




    }
}
