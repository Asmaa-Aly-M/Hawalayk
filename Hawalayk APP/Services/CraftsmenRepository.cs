using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;

namespace Hawalayk_APP.Services
{
    public class CraftsmenRepository : ICraftsmenRepository
    {

        private readonly ApplicationDbContext Context;
        private readonly ICraftRepository _craftService;
        //private readonly IPostRepository _postRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAddressService _addressService;



        public CraftsmenRepository(ICraftRepository craftService, ApplicationDbContext _Context,
            UserManager<ApplicationUser> userManager, IAddressService addressService)
        {
            Context = _Context;
            // _postRepository = postRepository;
            _userManager = userManager;
            _craftService = craftService;
            _addressService = addressService;
        }


        public async Task<List<PendingCraftsmanDTO>> GetPendingCraftsmen()
        {
            var pendingCraftsmen = await Context.Craftsmen
                                   .Where(c => c.RegistrationStatus == CraftsmanRegistrationStatus.Pending)
                                   .ToListAsync();

            var pendingCraftsmanDTO = pendingCraftsmen.Select(p =>
            {
                return new PendingCraftsmanDTO
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    BirthDate = p.BirthDate,
                    PhoneNumber = p.PhoneNumber,
                    Gender = p.Gender,
                    Governorate = p.Address.Governorate.governorate_name_ar,
                    City = p.Address.City.city_name_ar,
                    StreetName = p.Address.StreetName,
                    Craft = p.Craft.Name.ToString(),
                    PersonalImage = p.PersonalImage,
                    NationalIDImage = p.NationalIDImage
                };
            }).ToList();

            return pendingCraftsmanDTO;
        }

        public async Task<Craftsman> ApproveCraftsman(string id, bool isApproved)
        {
            var craftsman = await Context.Craftsmen.FirstOrDefaultAsync(c => c.Id == id);
            if (craftsman == null)
            {
                throw new ArgumentException("Craftsman not found");
            }

            if (isApproved)
                craftsman.RegistrationStatus = CraftsmanRegistrationStatus.Approved;
            else
                craftsman.RegistrationStatus = CraftsmanRegistrationStatus.Rejected;


            await Context.SaveChangesAsync();

            return craftsman;
        }


        public async Task<Craftsman> GetById(string id)
        {
            Craftsman Craftman = await Context.Craftsmen.Include(c => c.Craft).Include(a => a.Address).ThenInclude(city => city.City).ThenInclude(gov => gov.Governorate).FirstOrDefaultAsync(s => s.Id == id);
            return Craftman;//craft : 
        }

        public async Task<List<Craftsman>> GetAll()
        {
            return await Context.Craftsmen.ToListAsync();
        }


        public async Task<CraftsmanAccountDTO> GetCraftsmanAccountAsync(Craftsman craftsman)
        {

            var enumValue = (CraftName)craftsman.Craft.Name;

            var descriptionAttributes = typeof(CraftName)
                .GetField(enumValue.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                as DescriptionAttribute[];

            var craftName = descriptionAttributes?.Length > 0 ? descriptionAttributes[0].Description : "Description not found.";

            //
            return new CraftsmanAccountDTO
            {
                CraftsmanId = craftsman.Id,
                FirstName = craftsman.FirstName,
                LastName = craftsman.LastName,
                UserName = craftsman.UserName,
                ProfilePic = Path.Combine("imgs/", craftsman.ProfilePicture),
                BirthDate = craftsman.BirthDate,
                PhoneNumber = craftsman.PhoneNumber,
                CraftName = craftName,
                Rating = craftsman.Rating,
                City = craftsman.Address.City.city_name_ar,
                Governorate = craftsman.Address.Governorate.governorate_name_ar,
                street = craftsman.Address.StreetName
            };

        }
        public async Task<UpdateUserDTO> UpdateCraftsmanAccountAsync(string craftsmanId, CraftsmanUpdatedAccountDTO craftsmanAccount)

        {

            var craftsman = await GetById(craftsmanId);
            if (craftsman == null)
            {
                return new UpdateUserDTO { IsUpdated = false, Message = "Not Found : " };

            }


            var user = await _userManager.FindByNameAsync(craftsmanAccount.UserName);
            if (user != null && user.Id != craftsmanId)
            {
                return new UpdateUserDTO { IsUpdated = false, Message = "UserName is already Token : " };
            }

            string fileeName = user.ProfilePicture;
            if (craftsmanAccount.ProfilePic != null)
            {
                fileeName = craftsmanAccount.ProfilePic.FileName;
                string fileePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgs"));
                using (var fileStream = new FileStream(Path.Combine(fileePath, fileeName), FileMode.Create))
                {
                    craftsmanAccount.ProfilePic.CopyTo(fileStream);
                }
            }

            Craft craft = null;
            CraftName enumValue = (CraftName)ConvertToEnum<CraftName>(craftsmanAccount.CraftName);

            //if (Enum.TryParse(craftName, out craft_Name))
            //{
            craft = await Context.Crafts.FirstOrDefaultAsync(c => c.Name == enumValue);

            //}//parsing : enum 




            //var craft = await _craftService.GetOrCreateCraftAsync(craftsmanAccount.CraftName.ToString());

            craftsman.FirstName = craftsmanAccount.FirstName;
            craftsman.LastName = craftsmanAccount.LastName;
            craftsman.UserName = craftsmanAccount.UserName;
            craftsman.ProfilePicture = fileeName;
            craftsman.Craft = craft;
            craftsman.BirthDate = craftsmanAccount.BirthDate;
            craftsman.Address = await _addressService.CreateAsync(craftsmanAccount.Governorate, craftsmanAccount.City, craftsmanAccount.StreetName);


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

        public async Task<int> craftsmanNumber()
        {
            int counter = await Context.Craftsmen.CountAsync();
            return counter;
        }



        public async Task<List<GallaryPostDTO>> FilterMyCraftGallary(string craftsmanId)
        {
            Craftsman craftsman = await GetById(craftsmanId);
            Craft craft = craftsman.Craft;
            CraftName enumValue = craftsman.Craft.Name;
            string craftName = await _craftService.GetCraftNameInArabicByEnumValue(enumValue);
            /// craft = await Context.Crafts.FirstOrDefaultAsync(c => c.Name == enumValue);
            List<Post> posts = await Context.Posts
                .Include(c => c.Craftsman)
                .Where(s => s.CraftId == craft.Id &&
            (s.Flag == Enums.PostStatus.Gallery | s.Flag == Enums.PostStatus.Both) && s.CraftsmanId == craftsmanId).ToListAsync();

            // Convert Post objects to PostDTO objects
            List<GallaryPostDTO> postDTOs = posts.Select(post =>
            {
                return new GallaryPostDTO
                {
                    PostId = post.Id,
                    PostImgUrl = Path.Combine("imgs/", post.ImageURL),// Construct image URL
                    CraftsmanId = post.Craftsman.Id,
                    CraftsmanName = post.Craftsman.UserName,
                    Content = post.Content,
                    CraftsmanProfilePicUrl = Path.Combine("imgs/", post.Craftsman.ProfilePicture),
                    CraftName = craftName,
                    // Assuming Flag is an enum, convert it to string
                };
            }).ToList();

            return postDTOs;

        }








        private static T? ConvertToEnum<T>(string arabicString) where T : struct
        {
            Type enumType = typeof(T);

            if (enumType.IsEnum)
            {
                foreach (FieldInfo field in enumType.GetFields())
                {
                    if (Attribute.GetCustomAttribute(field,
                        typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                    {
                        if (attribute.Description == arabicString)
                        {
                            return (T)field.GetValue(null);
                        }
                    }
                }
            }
            return null;
        }

        /* public List<ServiceRequest> GetServiceRequestsByCraftName(CraftName craft)
         {

             var Requests = Context.ServiceRequests
                 .Where(request => request.craftName == craft)
                 .ToList();

             return Requests;
         }*/
    }
}