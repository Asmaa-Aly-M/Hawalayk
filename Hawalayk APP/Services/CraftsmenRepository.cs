﻿using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Reflection;

namespace Hawalayk_APP.Services
{
    public class CraftsmenRepository : ICraftsmenRepository
    {

        private readonly ApplicationDbContext Context;
        private readonly ICraftRepository _craftRepository;
        //private readonly IPostRepository _postRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAddressRepository _addressRepository;
        private readonly ISMSRepository _smsRepository;
        private readonly IFileService _fileService;


        public CraftsmenRepository(ICraftRepository craftRepository, ApplicationDbContext _Context,
            UserManager<ApplicationUser> userManager, IAddressRepository addressRepository, ISMSRepository smsRepository, IFileService fileService)
        {
            Context = _Context;
            // _postRepository = postRepository;
            _userManager = userManager;
            _craftRepository = craftRepository;
            _addressRepository = addressRepository;
            _smsRepository = smsRepository;
            _fileService = fileService;
        }


        public async Task<List<PendingCraftsmanDTO>> GetPendingCraftsmen()
        {
            var pendingCraftsmen = await Context.Craftsmen
                                   .Where(c => c.RegistrationStatus == CraftsmanRegistrationStatus.Pending)
                                   .Include(c => c.Address).ThenInclude(a => a.Governorate)
                                   .Include(c => c.Address).ThenInclude(a => a.City)
                                   .Include(c => c.Craft)
                                   .ToListAsync();

            var pendingCraftsmanDTO = pendingCraftsmen.Select(p =>
            {
                return new PendingCraftsmanDTO
                {
                    Id = p.Id,
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
        public async Task<Craftsman> GetByID(string id)
        {
            Craftsman craftsman = await Context.Craftsmen.SingleOrDefaultAsync(c => c.Id == id);
            return craftsman;
        }

        public async Task<List<Craftsman>> GetAll()
        {
            return await Context.Craftsmen.ToListAsync();
        }


        public async Task<CraftsmanAccountDTO> GetCraftsmanAccountAsync(string userId, string craftsmanId)
        {
            var craftsman = await Context.Craftsmen.Include(c => c.Reviews).FirstOrDefaultAsync(c => c.Id == craftsmanId);
            var enumValue = (CraftName)craftsman.Craft.Name;

            var descriptionAttributes = typeof(CraftName)
                .GetField(enumValue.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                as DescriptionAttribute[];

            var craftName = descriptionAttributes?.Length > 0 ? descriptionAttributes[0].Description : "Description not found.";

            bool isCraftsmanBlocked = Context.Blocks.Any(b => (b.BlockingUserId == userId && b.BlockedUserId == craftsman.Id) || (b.BlockedUserId == userId && b.BlockingUserId == craftsman.Id));
       
            return new CraftsmanAccountDTO
            {
                CraftsmanId = craftsman.Id,
                FirstName = craftsman.FirstName,
                LastName = craftsman.LastName,
                UserName = craftsman.UserName,
                ProfilePic = craftsman.ProfilePicture,
                PhoneNumber = craftsman.PhoneNumber,
                CraftName = craftName,
                Rating = craftsman.GetRating(),
                City = craftsman.Address.City.city_name_ar,
                Governorate = craftsman.Address.Governorate.governorate_name_ar,
                street = craftsman.Address.StreetName,
                isBlocked = isCraftsmanBlocked
            };

        }

        public async Task<UpdateUserDTO> UpdateCraftsmanAccountAsync(string craftsmanId, CraftsmanUpdatedAccountDTO craftsmanAccount)
        {

            var craftsman = await GetById(craftsmanId);
            if (craftsman == null)
            {
                return new UpdateUserDTO { IsUpdated = false, Message = "Not Found" };
            }


            var user = await _userManager.FindByNameAsync(craftsmanAccount.UserName);
            if (user != null && user.Id != craftsmanId)
            {
                return new UpdateUserDTO { IsUpdated = false, Message = "UserName is already Taken" };
            }

            var craft = await _craftRepository.GetOrCreateCraftAsync(craftsmanAccount.CraftName);

            craftsman.FirstName = craftsmanAccount.FirstName;
            craftsman.LastName = craftsmanAccount.LastName;
            craftsman.UserName = craftsmanAccount.UserName;
            craftsman.Craft = craft;
            craftsman.Address = await _addressRepository.CreateAsync(craftsmanAccount.Governorate, craftsmanAccount.City, craftsmanAccount.StreetName);


            var result = await _userManager.UpdateAsync(craftsman);

            if (!result.Succeeded)
            {
                return new UpdateUserDTO
                {
                    IsUpdated = false,
                    Message = "Failed to update"
                };
            }

            return new UpdateUserDTO { IsUpdated = true, Message = "The Account Updated Successfully" };
        }


        public async Task<UpdateUserDTO> UpdateCraftsmanProfilePicAsync(string craftsmanId, IFormFile profilePic)
        {
            var craftsman = await GetById(craftsmanId);
            if (craftsman == null)
            {
                return new UpdateUserDTO { IsUpdated = false, Message = "Not Found : " };

            }
            if (craftsman.ProfilePicLastUpdated != null && craftsman.ProfilePicLastUpdated.AddYears(1) > DateTime.UtcNow)
            {
                return new UpdateUserDTO { IsUpdated = false, Message = "Profile picture can only be updated once a year." };
            }

            if (profilePic == null)
            {
                return new UpdateUserDTO { IsUpdated = false, Message = "No profile picture provided." };
            }

         
            var profilePicturePath = await _fileService.SaveFileAsync(profilePic, "ProfilePictures");


            craftsman.ProfilePicture = profilePicturePath;
            craftsman.ProfilePicLastUpdated = DateTime.Now;

            var result = await _userManager.UpdateAsync(craftsman);

            if (!result.Succeeded)
            {
                return new UpdateUserDTO
                {
                    IsUpdated = false,
                    Message = "Failed to update profile picture."
                };
            }

            return new UpdateUserDTO { IsUpdated = true, Message = "Profile picture updated successfully." };

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
            string craftName = await _craftRepository.GetCraftNameInArabicByEnumValue(enumValue);
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
                    PostImgUrl = post.ImageURL,// Construct image URL
                    CraftsmanId = post.Craftsman.Id,
                    CraftsmanName = post.Craftsman.UserName,
                    Content = post.Content,
                    CraftsmanProfilePicUrl = post.Craftsman.ProfilePicture,
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

        /*public async Task<List<ServiceRequest>> GetServiceRequestsByCraftName(CraftName craftName)
        {

            
            var requests = Context.ServiceRequests
                .Where(request => request.craftName == craft && 
                request.jobApplications.
                Where(jobApplication=>jobApplication.ResponseStatus==ResponseStatus.Accepted).ToList()==null)
                .ToList();


            var requests =await Context.ServiceRequests
                .Where(request => request.craft.Name == craftName &&
               request.JobApplication.Where(JobApplication => JobApplication.ResponseStatus == ResponseStatus.Accepted).ToList()== null).ToList();
            return requests;
        }*/
        //service request => no job app : refused job app

        public async Task<List<AvailableServiceRequestDTO>> GetAvailableServiceRequestsByCraft(CraftName craftName)//بالنسبة للحرفي
        {
            var requests = await Context.ServiceRequests
                .Include(r=>r.Customer)
                .Include(r=>r.craft)
                .Include(r=>r.JobApplications)
                .Where(request => request.craft.Name == craftName).ToListAsync();

            var filteredRequests = requests
                .Where(request => !request.JobApplications.Any(ja => ja.ResponseStatus == ResponseStatus.Accepted)).ToList();
            List<AvailableServiceRequestDTO> serviceNeededRepaly = filteredRequests.Select(x =>
               new AvailableServiceRequestDTO
               {

                   CustomerID = x.CustomerId,
                   CustomerFirstName = x.Customer.FirstName,
                   CustomerLastName = x.Customer.LastName,
                   CustomerProfilePicture = x.Customer.ProfilePicture,
                   Content = x.Content,
                   OptionalImage = x.OptionalImage,
                   Governorate = x.governorate,
                   City = x.city,
                   Street = x.street,
                   ServiceRequestId=x.Id,
                   DatePosted=x.DatePosted,


               }).ToList();
            return serviceNeededRepaly;

        }



        public async Task<List<RequestAcceptedForCraftsmanDTO>> GetAcceptedServiceRequestsFromCustomersByACraftsman(string craftsmanID)//بالنسبة للحرفي
        {
            var AlljopApplications = await Context.JobApplications.Include(j => j.ServiceRequest).ThenInclude(s => s.Customer)
                .Where(x => x.CraftsmanId == craftsmanID &&
            x.ResponseStatus == ResponseStatus.Accepted).ToListAsync();


            List<RequestAcceptedForCraftsmanDTO> RequestAcceptedCraftsman = AlljopApplications.Select(y =>
               new RequestAcceptedForCraftsmanDTO
               {

                   CustomerID = y.ServiceRequest.CustomerId,
                   CustomerFristName = y.ServiceRequest.Customer.FirstName,
                   CustomerLastName = y.ServiceRequest.Customer.LastName,
                   CustomerProfilePicture = y.ServiceRequest.Customer.ProfilePicture,
                   Content = y.ServiceRequest.Content,
                   OptionalImage = y.ServiceRequest.OptionalImage,
                   ServiceRequestId = y.ServiceRequest.Id,
                   DatePosted = y.ServiceRequest.DatePosted,
                   JobApplicationId=y.Id,
                   CraftsmanID=y.CraftsmanId,


               }).ToList();
            return RequestAcceptedCraftsman;

        }

        /* public List<ServiceRequest> GetServiceRequestsByCraftName(CraftName craft)
         {

             var Requests = Context.ServiceRequests
                 .Where(request => request.craftName == craft)
                 .ToList();

             return Requests;
         }حلاص مش محتاجينها */
    }
}