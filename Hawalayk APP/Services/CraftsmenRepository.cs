using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
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




        public  async Task<Craftsman> GetById(string id)
        {
            Craftsman Craftman = await Context.Craftsmen.Include(c=>c.Craft).FirstOrDefaultAsync(s => s.Id == id);
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

        public async Task<CraftsmanAccountDTO> GetCraftsmanAccountAsync(Craftsman craftsman)
        {
            
            return new CraftsmanAccountDTO
            {
                FirstName = craftsman.FirstName,
                LastName = craftsman.LastName,
                UserName = craftsman.UserName,
                ProfilePic= craftsman.ProfilePicture,
                BirthDate = craftsman.BirthDate,
               // PhoneNumber = craftsman.PhoneNumber,
                CraftName = Enum.GetName(typeof(CraftName), craftsman.Craft.Name)

            };
            
        }
        //async Task<CraftsmanAccountDTO> UpdateCraftsmanAccountAsync(Craftsman craftsman, CraftsmanAccountDTO craftsmanAccount)
        //{
        //    craftsman.FirstName = craftsmanAccount.FirstName;
        //    craftsman.LastName = craftsmanAccount.LastName;         
        //    craftsman.UserName = craftsmanAccount.UserName;
        //    craftsman.Craft 
        //}



    }
}
