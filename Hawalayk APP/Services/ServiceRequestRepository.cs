using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Services
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        ApplicationDbContext Context;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICraftRepository _craftRepository;
        public ServiceRequestRepository(ApplicationDbContext _Context, ICustomerRepository customerRepository, ICraftRepository craftRepository)
        {
            Context = _Context;
            _customerRepository = customerRepository;
            _craftRepository = craftRepository;
        }

        public async Task<ServiceRequestSendDTO> GetServiceRequestSend(int id)
        {
            var serviceRequest = await GetById(id);
            return new ServiceRequestSendDTO
            {

                CustomerId = serviceRequest.CustomerId,
                CustomerFirstName = serviceRequest.Customer.FirstName,
                CustomerLastName = serviceRequest.Customer.LastName,
                CustomerImg = Path.Combine("imgs/", serviceRequest.Customer.ProfilePicture),
                CustomerUserName = serviceRequest.Customer.UserName,
                Content = serviceRequest.Content,
                ServiceRequestId = serviceRequest.Id,
                ServiceRequestImg = Path.Combine("imgs/", serviceRequest.OptionalImage)
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


        public async Task<int> CreateAsync(string customerId, ServiceRequestDTO newservice)
        {
            Customer customer = await _customerRepository.GetByIdAsync(customerId);

            if (customer == null)
            {
                return -1;
            }
            var file = newservice.optionalImage;

            string fileName = "";
            if (file != null)
            {
                fileName = file.FileName;
                string filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgs"));
                using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            var CrafEnumVAlue = await _craftRepository.GetEnumValueOfACraftByArabicDesCription(newservice.craftName);
            ServiceRequest serviceRequest = new ServiceRequest()
            {
                //  Id = newservice.Id,
                governorate = newservice.governorate,
                city = newservice.city,
                street = newservice.street,
                Content = newservice.content,
                OptionalImage = fileName, // IFormFIle
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
        public List<ServiceRequest> GetLatestServiceRequests()
        {
            // Query the database for the latest 5 service requests
            var latestRequests = Context.ServiceRequests
                .OrderByDescending(sr => sr.DatePosted)
                .Take(5)
                .ToList();

            return latestRequests;
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



        public async Task<List<ServiceNeededRepalyForCustomerDTO>> GetServiceRequestsNeedToReplayByCraftsmenForCustomer(string customerId)//بالنسبة للعميل
        {
            var requests = await Context.ServiceRequests
                .Where(request => request.CustomerId == customerId &&
               request.JobApplications.Where(JobApplication => JobApplication.ResponseStatus == ResponseStatus.Accepted).ToList() == null)
                .ToListAsync();
            List<ServiceNeededRepalyForCustomerDTO> serviceNeededRepaly = (await Task.WhenAll(requests.Select(async x =>
               new ServiceNeededRepalyForCustomerDTO
               {
                   ServiceRequestId = x.Id,
                   ServiceContent = x.Content,
                   ServiceCraftName = await _craftRepository.GetCraftNameInArabicByEnumValue(x.craft.Name),
                   Date = x.DatePosted,

               }))).ToList();
            return serviceNeededRepaly;

        }
        public async Task<List<RequestAcceptedForCustomrDTO>> GetServiceRequestsAcceptedCraftsmenForCustomer(string customerID)//بالنسبة للعميل
        {
            var AlljopApplications = await Context.JobApplications.Include(j => j.ServiceRequest)
                .Where(x => x.ServiceRequest.CustomerId == customerID && x.ResponseStatus == ResponseStatus.Accepted).ToListAsync();

            List<RequestAcceptedForCustomrDTO> RequestAcceptedCraftsman = (await Task.WhenAll(AlljopApplications.Select(async y => //حل مشكلة انه مش كان قادر يستخدم تحويل ال enum
               new RequestAcceptedForCustomrDTO
               {
                   ServiceRequestId = y.ServiceRequestId,
                   CraftName = await _craftRepository.GetCraftNameInArabicByEnumValue(y.ServiceRequest.craft.Name),
                   ServiceContent = y.ServiceRequest.Content,
                   CraftsmanFristName = y.Craftsman.FirstName,
                   CraftsmanLastName = y.Craftsman.LastName,
                   Date = y.ServiceRequest.DatePosted,////محتاج مراجعة نوع الوقت في ال DTO



               }))).ToList();
            return RequestAcceptedCraftsman;

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



    }
}
