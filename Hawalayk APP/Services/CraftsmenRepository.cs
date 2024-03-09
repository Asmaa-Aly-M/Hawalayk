using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace Hawalayk_APP.Services
{
    public class CraftsmenRepository : ICraftsmenRepository
    {

        ApplicationDbContext Context;
        private readonly ICraftRepository _craftService;
        //private readonly IPostRepository _postRepository;
        private readonly UserManager<ApplicationUser> _userManager;



        public CraftsmenRepository(ICraftRepository craftService, ApplicationDbContext _Context, UserManager<ApplicationUser> userManager)
        {
            Context = _Context;
            // _postRepository = postRepository;
            _userManager = userManager;
            _craftService = craftService;

        }




        public Craftsman GetById(string id)
        {
            Craftsman Craftman = Context.Craftsmen.FirstOrDefault(s => s.Id == id);
            return Craftman;
        }

        /* public async Task<Craftsman> GetById(string id)
         {
             Craftsman Craftman = await Context.Craftsmen.Include(c => c.Craft).FirstOrDefaultAsync(s => s.Id == id);
             return Craftman;
         }*/

        public List<Craftsman> GetAll()
        {
            return Context.Craftsmen.ToList();
        }


        public async Task<CraftsmanAccountDTO> GetCraftsmanAccountAsync(Craftsman craftsman)
        {

            return new CraftsmanAccountDTO
            {
                FirstName = craftsman.FirstName,
                LastName = craftsman.LastName,
                UserName = craftsman.UserName,
                ProfilePic = craftsman.ProfilePicture,
                BirthDate = craftsman.BirthDate,
                // PhoneNumber = craftsman.PhoneNumber,
                CraftName = Enum.GetName(typeof(CraftName), craftsman.Craft.Name)

            };

        }
        public async Task<UpdateUserDTO> UpdateCraftsmanAccountAsync(string craftsmanId, CraftsmanAccountDTO craftsmanAccount)

        {

            var craftsman = GetById(craftsmanId);
            if (craftsman == null)
            {
                return new UpdateUserDTO { IsUpdated = false, Message = "Not Found : " };

            }


            var user = await _userManager.FindByNameAsync(craftsmanAccount.UserName);
            if (user != null && user.Id != craftsmanId)
            {
                return new UpdateUserDTO { IsUpdated = false, Message = "UserName is already Token : " };
            }

            var craft = await _craftService.GetOrCreateCraftAsync(craftsmanAccount.CraftName.ToString());

            craftsman.FirstName = craftsmanAccount.FirstName;
            craftsman.LastName = craftsmanAccount.LastName;
            craftsman.UserName = craftsmanAccount.UserName;
            craftsman.ProfilePicture = craftsmanAccount.ProfilePic;
            craftsman.Craft = craft;
            craftsman.BirthDate = craftsmanAccount.BirthDate;


            var result = await _userManager.UpdateAsync(craftsman);
            if (!result.Succeeded)
            {
                return new UpdateUserDTO
                {
                    IsUpdated = false,
                    Message = "Failed to update"
                };
            }
            return new UpdateUserDTO { IsUpdated = true, Message = "The Account Updated Successfully :" };


        }


    }
}
