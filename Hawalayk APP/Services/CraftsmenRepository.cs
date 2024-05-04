﻿using Hawalayk_APP.Context;
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



        public CraftsmenRepository(ICraftRepository craftService, ApplicationDbContext _Context, UserManager<ApplicationUser> userManager)
        {
            Context = _Context;
            // _postRepository = postRepository;
            _userManager = userManager;
            _craftService = craftService;

        }
















        public async Task<List<Craftsman>> GetPendingCraftsmen()
        {
            return await Context.Craftsmen
                                   .Where(c => c.RegistrationStatus == CraftsmanRegistrationStatus.Pending)
                                   .ToListAsync();
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





        public Craftsman GetById(string id)
        {
            Craftsman Craftman = Context.Craftsmen.Include(c => c.Craft).Include(a => a.Address).ThenInclude(city => city.City).ThenInclude(gov => gov.Governorate).FirstOrDefault(s => s.Id == id);
            return Craftman;//craft : 
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
                // PhoneNumber = craftsman.PhoneNumber,
                CraftName = craftName,
                Rating = craftsman.Rating,
                City = craftsman.Address.City.city_name_ar,
                Governorate = craftsman.Address.Governorate.governorate_name_ar,
                street = craftsman.Address.StreetName

            };

        }
        public async Task<UpdateUserDTO> UpdateCraftsmanAccountAsync(string craftsmanId, CraftsmanUpdatedAccountDTO craftsmanAccount)

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

        public int craftsmanNumber()
        {
            int counter = Context.Craftsmen.Count();
            return counter;
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


    }
}
