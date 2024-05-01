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

        public int Create(string customerId, ServiceRequestDTO newservice)
        {
            Customer customer = customerRepo.GetById(customerId);
            ServiceRequest serviceRequest = new ServiceRequest()
            {
                //  Id = newservice.Id,
                Content = newservice.Content,
                OptionalImage = newservice.OptionalImage, // IFormFIle
                CustomerId = customer.Id,

            };
            Context.ServiceRequests.Add(serviceRequest);
            int row = Context.SaveChanges();
            return row;
        }

        public int Delete(int id)
        {
            ServiceRequest Oldservice = Context.ServiceRequests.FirstOrDefault(s => s.Id == id);
            Context.ServiceRequests.Remove(Oldservice);
            int row = Context.SaveChanges();
            return row;
        }
    }
}
