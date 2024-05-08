using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Services
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        ApplicationDbContext Context;
        private readonly ICustomerRepository customerRepo;
        public ServiceRequestRepository(ApplicationDbContext _Context, ICustomerRepository _customerRepo)
        {
            Context = _Context;
            customerRepo = _customerRepo;

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
        //    //Customer customer = await customerRepo.GetByIdAsync(customerId);
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
            Customer customer = await customerRepo.GetByIdAsync(customerId);

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
            ServiceRequest serviceRequest = new ServiceRequest()
            {
                //  Id = newservice.Id,
                Content = newservice.content,
                OptionalImage = fileName, // IFormFIle
                CustomerId = customerId,

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
    }
}
