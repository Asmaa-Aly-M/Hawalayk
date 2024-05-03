using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

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




        public ServiceRequest GetById(int id)
        {
            ServiceRequest service = Context.ServiceRequests.FirstOrDefault(s => s.Id == id);
            return service;
        }
        public List<ServiceRequest> GetAll()
        {
            return Context.ServiceRequests.ToList();
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
            ServiceRequest serviceRequest = new ServiceRequest()
            {
                //  Id = newservice.Id,
                Content = newservice.content,
                OptionalImage = newservice.optionalImage, // IFormFIle
                CustomerId = customerId,

            };
            Context.ServiceRequests.Add(serviceRequest);
            int row = await Context.SaveChangesAsync();
            return row;
        }

        public int Delete(int id)
        {
            ServiceRequest Oldservice = Context.ServiceRequests.FirstOrDefault(s => s.Id == id);
            Context.ServiceRequests.Remove(Oldservice);
            int row = Context.SaveChanges();
            return row;
        }

        public int countService()
        {
            int counter = Context.ServiceRequests.Count();
            return counter;
        }
    }
}
