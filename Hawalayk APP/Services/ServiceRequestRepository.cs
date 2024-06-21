using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Hawalayk_APP.Services
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        ApplicationDbContext Context;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICraftRepository _craftRepository;
        private readonly IFileService _fileService;
        private readonly IBlockingRepository _blockingRepository;
        public ServiceRequestRepository(ApplicationDbContext _Context, ICustomerRepository customerRepository,
            ICraftRepository craftRepository, IFileService fileService, IBlockingRepository blockingRepository)
        {
            Context = _Context;
            _customerRepository = customerRepository;
            _craftRepository = craftRepository;
            _fileService = fileService;
            _blockingRepository = blockingRepository;
        }

        public async Task<ServiceRequestSendDTO> GetServiceRequestSend(int id)
        {
            var serviceRequest = await GetById(id);
            return new ServiceRequestSendDTO
            {

                CustomerId = serviceRequest.CustomerId,
                CustomerFirstName = serviceRequest.Customer.FirstName,
                CustomerLastName = serviceRequest.Customer.LastName,
                CustomerImg = serviceRequest.Customer.ProfilePicture,
                CustomerUserName = serviceRequest.Customer.UserName,
                Content = serviceRequest.Content,
                ServiceRequestId = serviceRequest.Id,
                ServiceRequestImg = serviceRequest.OptionalImage
            };
        }

        public async Task<ServiceRequest> GetById(int id)
        {
            ServiceRequest service = await Context.ServiceRequests.Include(c => c.Customer).FirstOrDefaultAsync(s => s.Id == id);
            return service;
        }
        public async Task<List<ServiceRequest>> GetAll()
        {
            return await Context.ServiceRequests.ToListAsync();
        }
        //public async Task<int> CreateAsync(string customerId, ServiceRequestDTO newservice)
        //{
        //    // Validate customerId
        //    if (string.IsNullOrEmpty(customerId))
        //    {
        //        throw new ArgumentNullException(nameof(customerId), "Customer ID cannot be null or empty.");
        //    }

        //    // Get the customer
        //    //Customer customer = await _customerRepository.GetByIdAsync(customerId);
        //    //if (customer == null)
        //    //{
        //    //    throw new ArgumentException($"Customer with ID '{customerId}' not found.", nameof(customerId));
        //    //}

        //    // Create the ServiceRequest object
        //    ServiceRequest serviceRequest = new ServiceRequest()
        //    {
        //        OptionalImage = newservice.optionalImage,
        //        Content = newservice.content,

        //        CustomerId = customerId,
        //    };

        //    // Handle optional image if provided
        //    //if (newservice.optionalImage != null)
        //    //{
        //    //    // Process the optional image (save to file system, database, etc.)
        //    //    // For example, to save to a file:
        //    //    // string imagePath = await SaveImage(newservice.optionalImage);
        //    //    // serviceRequest.OptionalImage = imagePath;
        //    //}

        //    // Add the ServiceRequest to the context and save changes
        //    Context.ServiceRequests.Add(serviceRequest);
        //    int rowsAffected = await Context.SaveChangesAsync();

        //    return rowsAffected;
        //}


        public async Task<int> CreateAsync(string craftName, string customerId, ServiceRequestDTO newservice)
        {
            var enumValueOfCraft = await _craftRepository.GetEnumValueOfACraftByArabicDesCription(craftName);
            var craftsmenOfCraft = Context.Craftsmen.Include(c => c.Craft)
                .Where(c => c.Craft.Name == enumValueOfCraft).ToList();
            if (craftsmenOfCraft == null)
            {
                return 0;
            }

            Customer customer = await _customerRepository.GetByIdAsync(customerId);

            if (customer == null)
            {
                return -1;
            }

            var serviceRequestImagePath = await _fileService.SaveFileAsync(newservice.optionalImage, "ServiceRequestImages");

            var CrafEnumVAlue = await _craftRepository.GetEnumValueOfACraftByArabicDesCription( craftName);

            ServiceRequest serviceRequest = new ServiceRequest()
            {
                governorate = newservice.governorate,
                city = newservice.city,
                street = newservice.street,
                Content = newservice.content,
                OptionalImage = serviceRequestImagePath, 
                CustomerId = customerId,
                craftName = CrafEnumVAlue,
                CraftId = await _craftRepository.GetCraftIdByCraftEnumValue(CrafEnumVAlue)

            };

            Context.ServiceRequests.Add(serviceRequest);

            int row = await Context.SaveChangesAsync();

            if (row > 0)
            {
                return serviceRequest.Id;
            }
            else
            {
                return -1;
            }
            // ServiceRequestSendDTO  serviceRequestSendDTO = new ServiceRequestSendDTO()
            //if(row > 0)
            //{
            //    serviceRequestSendDTO = GetServiceRequestByCustomerIdAndDateTimePosted(customerId,)
            //}
            // return serviceRequest;
            //        return row;
        }
        //private async Task<ServiceRequestSendDTO> GetServiceRequestByCustomerIdAndDateTimePosted(string customerId, DateTime dateTime)
        //{
        //    var serviceRequest = await Context.ServiceRequests.Include(s => s.Customer).FirstOrDefaultAsync(c => c.CustomerId == customerId && c.DatePosted == dateTime);
        //    return new ServiceRequestSendDTO
        //    {

        //        CustomerId = customerId,
        //        CustomerFirstName = serviceRequest.Customer.FirstName,
        //        CustomerLastName = serviceRequest.Customer.LastName,
        //        CustomerImg = serviceRequest.Customer.ProfilePicture,
        //        CustomerUserName = serviceRequest.Customer.UserName,
        //        Content = serviceRequest.Content,
        //        ServiceRequestId = serviceRequest.Id,
        //        ServiceRequestImg = serviceRequest.OptionalImage
        //    };
        //}

        public async Task<int> Delete(int id)
        {
            ServiceRequest Oldservice = await Context.ServiceRequests.FirstOrDefaultAsync(s => s.Id == id);
            Context.ServiceRequests.Remove(Oldservice);
            int row = await Context.SaveChangesAsync();
            return row;
        }

        public async Task<int> countService()
        {
            int counter = await Context.ServiceRequests.CountAsync();
            return counter;
        }
        /*  public List<ServiceRequest> GetLatestServiceRequests()
          {
              // Query the database for the latest 5 service requests
              var latestRequests = Context.ServiceRequests
                  .OrderByDescending(sr => sr.DatePosted)
                  .Take(5)
                  .ToList();

              return latestRequests;
          }*/
        public List<RequestForDashBord> GetLatestServiceRequests()
        {
            // Query the database for the latest 5 service requests
            var latestRequests = Context.ServiceRequests
                 .Include(sr => sr.Customer)
            .Include(sr => sr.JobApplications)
                .ThenInclude(ja => ja.Craftsman)
                 .OrderByDescending(sr => sr.DatePosted)
                .Take(5)
                .ToList();

            //var requests = await Context.ServiceRequests

            // if (latestRequests == null)
            // {
            //    return null;
            // }

            List<RequestForDashBord> service = latestRequests.Select(x =>

            new RequestForDashBord
            {

                id = x.Id,
                Content = x.Content,
                CustomerId = x.CustomerId,
                CustomerFristName = x.Customer.FirstName,
                CustomerLastName = x.Customer.LastName,
                DatePosted = x.DatePosted,
                ResponseStatus = x.JobApplications.FirstOrDefault()?.ResponseStatus,
                CraftsmanFristName = x.JobApplications.FirstOrDefault()?.Craftsman.FirstName,
                CraftsmanLastName = x.JobApplications.FirstOrDefault()?.Craftsman.LastName,
            }).ToList();

            return service;
        }
        public int CountUsersMakingRequestsToday()
        {
            // Calculate the start of today
            DateTime today = DateTime.Today;

            // Count distinct users who made service requests today
            int count = Context.ServiceRequests
                .Where(sr => sr.DatePosted >= today)
                .Select(sr => sr.CustomerId)
                .Distinct()
                .Count();

            return count;
        }
        public double GetPercentageOfUsersMakingRequestsToday()
        {


            // Get the count of distinct users who made service requests today
            int usersMakingRequestsToday = CountUsersMakingRequestsToday();


            // Get the total number of customers
            int totalCustomers = Context.Customers.Count();

            // Calculate the percentage
            double percentage = 0;
            if (totalCustomers > 0)
            {
                percentage = (double)usersMakingRequestsToday / totalCustomers * 100;
            }

            return percentage;
        }

        public int CountUsersMakingRequestsLastWeek()
        {
            // Calculate the start of the week (last Sunday)
            DateTime lastSunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);

            // Count distinct users who made service requests last week
            int count = Context.ServiceRequests
                .Where(sr => sr.DatePosted >= lastSunday && sr.DatePosted < lastSunday.AddDays(7))
                .Select(sr => sr.CustomerId)
                .Distinct()
                .Count();

            return count;
        }

        public int CountUsersMakingRequestsLastMonth()
        {
            // Calculate the start of the last month
            DateTime lastMonthStart = DateTime.Today.AddMonths(-1).AddDays(1 - DateTime.Today.Day);

            // Count distinct users who made service requests last month
            int count = Context.ServiceRequests
                .Where(sr => sr.DatePosted >= lastMonthStart && sr.DatePosted < DateTime.Today)
                .Select(sr => sr.CustomerId)
                .Distinct()
                .Count();

            return count;
        }



        public async Task<List<ServiceNeededRepalyForCustomerDTO>> GetServiceRequestsNeedToReplayByCraftsmenForCustomer(string customerId)
        {
            // Retrieve service requests that have no accepted job applications for the specified customer ID
            var requests = await Context.ServiceRequests
                .Include(j=>j.JobApplications)
                .Include(c=>c.craft)
                .Where(request => request.CustomerId == customerId &&
                                  !request.JobApplications.Any(jobApplication => jobApplication.ResponseStatus == ResponseStatus.Accepted))
                .ToListAsync();
            // Create a list of DTOs for the service requests needing a replay
            var serviceNeededReplay = await Task.WhenAll(requests.Select(async request =>
                new ServiceNeededRepalyForCustomerDTO
                {
                    ServiceRequestId = request.Id,
                    CustomerId = request.CustomerId,
                    ServiceContent = request.Content,
                    OptionalImage=request.OptionalImage,
                    ServiceCraftName = await _craftRepository.GetCraftNameInArabicByEnumValue(request.craft.Name),
                    Date = request.DatePosted
                }
            ));

            return serviceNeededReplay.ToList();
        }

        public async Task<List<RequestAcceptedForCustomrDTO>> GetServiceRequestsAcceptedCraftsmenForCustomer(string customerID)//بالنسبة للعميل
        {
            var requests = await Context.ServiceRequests
                .Include(j => j.JobApplications)
                    .ThenInclude(c => c.Craftsman)
                .Include(c => c.craft)
                .Where(request => request.CustomerId == customerID &&
                                  request.JobApplications.Any(jobApplication => jobApplication.ResponseStatus == ResponseStatus.Accepted))
                .ToListAsync();
            if (requests == null) 
            {
                return null;
            }

            var requestAcceptedCraftsman = await Task.WhenAll(requests.Select(async s =>
            {
                var acceptedJobApplication = s.JobApplications
                    .FirstOrDefault(ja => ja.ResponseStatus == ResponseStatus.Accepted);

                return new RequestAcceptedForCustomrDTO
                {
                    ServiceRequestId = s.Id,
                    CutomerId=s.CustomerId,
                    CraftName = await _craftRepository.GetCraftNameInArabicByEnumValue(s.craft.Name),
                    ServiceContent = s.Content,
                    OptionalImage=s.OptionalImage,
                    JobApplicationId= acceptedJobApplication.Id,
                    CraftsmanId = acceptedJobApplication?.Craftsman.Id,
                    CraftsmanFristName = acceptedJobApplication?.Craftsman.FirstName,
                    CraftsmanLastName = acceptedJobApplication?.Craftsman.LastName,
                    Date = s.DatePosted
                };
            }
     ));

            return requestAcceptedCraftsman.ToList();

        }








        /* public async Task<List<ServiceNeededRepalyDTO>> GetServiceRequestsAcceptedCraftsmenForCustomer(string customerId)//بالنسبة للعميل
         {
             var requests = await Context.ServiceRequests
                 .Where(request => request.CustomerId == customerId && request.JobApplications.Where(JobApplication => JobApplication.ResponseStatus == ResponseStatus.Accepted).ToList() != null)
                 .ToListAsync();
             List<ServiceNeededRepalyDTO> serviceNeededRepaly = requests.Select(x =>
                new ServiceNeededRepalyDTO
                {

                    CustomerID = x.CustomerId,
                    CustomerFristName = x.Customer.FirstName,
                    CustomerLastName = x.Customer.LastName,
                    CustomerProfilePicture = x.Customer.ProfilePicture,
                    Content = x.Content,
                    OptionalImage = x.OptionalImage,
                    Governorate = x.Governorate,
                    City = x.City,
                    Street = x.Street,


                }).ToList();
             return serviceNeededRepaly;

         }*/ //مراجعة



        public async Task<List<JobApplication>> getAllJopapplicatoinForAServicceRequest(int serviceId)
        {
            var jobApplications = Context.JobApplications.Where(s => s.ServiceRequestId == serviceId).ToList();
            return jobApplications;

        }

        //Excluding blocked users
        public async Task<List<AvailableServiceRequestDTO>> GetAvailableServiceRequestsByCraft(CraftName craftName, string craftsmanId)
        {

            // Retrieve the list of blocked users for the current craftsman
            var blockedUserIds = await _blockingRepository.GetBlockedUsersAsync(craftsmanId);

            // Fetch all service requests for the specified craft
            var requests = await Context.ServiceRequests
                .Include(r => r.Customer)
                .Include(r => r.craft)
                .Include(r => r.JobApplications)
                .Where(request => request.craft.Name == craftName).ToListAsync();

            // Filter out requests from blocked users and those with an accepted job application
            var filteredRequests = requests
                .Where(request =>
                    !blockedUserIds.Contains(request.CustomerId) &&
                    !request.JobApplications.Any(ja => ja.ResponseStatus == ResponseStatus.Accepted))
                .ToList();

            // Convert the filtered requests to availableServiceRequests objects
            List<AvailableServiceRequestDTO> availableServiceRequests = filteredRequests.Select(x =>
               new AvailableServiceRequestDTO
               {
                   ServiceRequestId = x.Id,
                   CustomerID = x.CustomerId,
                   CustomerFirstName = x.Customer.FirstName,
                   CustomerLastName = x.Customer.LastName,
                   CustomerProfilePicture = x.Customer.ProfilePicture,
                   Content = x.Content,
                   OptionalImage = x.OptionalImage,
                   Governorate = x.governorate,
                   City = x.city,
                   Street = x.street,
               }).ToList();

            return availableServiceRequests;
        }

    }
}
