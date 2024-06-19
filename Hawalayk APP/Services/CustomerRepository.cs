using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Services
{

    public class CustomerRepository : ICustomerRepository

    {
        ApplicationDbContext _context;
        private readonly IBlockingRepository _blockingRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAddressRepository _addressRepository;
        private readonly IFileService _fileService;
        public CustomerRepository(ApplicationDbContext context, IBlockingRepository blockingRepository, UserManager<ApplicationUser> userManager, IFileService fileService, IAddressRepository addressRepository)
        {
            _context = context;
            _blockingRepository = blockingRepository;
            _userManager = userManager;
            _fileService = fileService;
            _addressRepository = addressRepository;

        }

        public async Task<List<Customer>> GetAll()
        {
            List<Customer> customers = await _context.Customers.ToListAsync();

            return customers;


        }


        public async Task<CustomerAccountDTO> GetCustomerAccountAsync(string customerId)
        {
            var customer = await _context.Customers.Include(c => c.Address).ThenInclude(A => A.Governorate).Include(c =>c.Address).ThenInclude(A => A.City).FirstOrDefaultAsync(c => c.Id == customerId);
            var customerAccountDTO = new CustomerAccountDTO()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                UserName = customer.UserName,
                ProfilePic = customer.ProfilePicture,
                //BirthDate = customer.BirthDate,
                PhoneNumber = customer.PhoneNumber,
                Governorate = customer.Address.Governorate.governorate_name_ar,
                City = customer.Address.City.city_name_ar,
                StreetName = customer.Address.StreetName
            };


            return customerAccountDTO;

        }


        public async Task<UpdateUserDTO> UpdateCustomerAccountAsync(string customerId, UpdateCustomerAccountDTO customerAccount)
        {
            var customer = await _context.Customers.Include(c => c.Address).ThenInclude(A => A.Governorate).Include(c => c.Address).ThenInclude(A => A.City).FirstOrDefaultAsync(c => c.Id == customerId);

            if (customer == null)
            {
                return new UpdateUserDTO { IsUpdated = false, Message = "Not Found" };
            }


            var user = await _userManager.FindByNameAsync(customerAccount.UserName);
            if (user != null && user.Id != customerId)
            {
                return new UpdateUserDTO { IsUpdated = false, Message = "UserName is already Taken" };
            }


            customer.FirstName = customerAccount.FirstName;
            customer.LastName = customerAccount.LastName;
            customer.UserName = customerAccount.UserName;
            if(customerAccount.ProfilePic != null)
            {
                var profilePicturePath = await _fileService.SaveFileAsync(customerAccount.ProfilePic, "ProfilePictures");

                customer.ProfilePicture = profilePicturePath;
            }
            
            customer.Address = await _addressRepository.CreateAsync(customerAccount.Governorate, customerAccount.City, customerAccount.StreetName);


            var result = await _userManager.UpdateAsync(customer);

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

        public async Task<Customer> GetByIdAsync(string id)
        {
            Customer customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            return customer;
        }

        public async Task<int> customerNumber()
        {
            int counter = await _context.Customers.CountAsync();
            return counter;
        }

        /* public async Task<List<ServiceRequest>> GetServiceRequestsForThisCustomer(string customerID)
         {

             var Requests = await _context.ServiceRequests
                 .Where(request => request.CustomerId == customerID)
                 .ToListAsync();

             return Requests;
         } هنركن دى هنا لغاية لما نشوف هنحتاجها ولا لا لان محدش عارف بس محدش يمسحهاهى بترجع كل ال service ا عملها العميل 
        */



        public async Task<List<SearchAboutCraftsmanDTO>> searchAboutCraftsmen(string userId, CraftName craftName, string governorate)
        {
            var Allcraftsmen = await _context.Craftsmen
                .Include(c=>c.Craft)
                .Include(g => g.Address)
                    .ThenInclude(g=>g.Governorate)
                .Where(c => c.Craft.Name == craftName && c.Address.Governorate.governorate_name_ar == governorate).ToListAsync();

            var blockedUserIds = await _blockingRepository.GetBlockedUsersAsync(userId);

            List<SearchAboutCraftsmanDTO> Craftsmen = Allcraftsmen.Select(y =>
               new SearchAboutCraftsmanDTO
               {
                   Id = y.Id,
                   FirstName = y.FirstName,
                   LastName = y.LastName,
                   Rating = y.Rating,
                   ProfilePicture = y.ProfilePicture,

               }).ToList();

            return Craftsmen;

        }

    }
}
