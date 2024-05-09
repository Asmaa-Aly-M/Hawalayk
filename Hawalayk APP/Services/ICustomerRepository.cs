using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface ICustomerRepository
    {
        Task<CustomerAccountDTO> GetCustomerAccountAsync(Customer customer);
        Task<List<Customer>> GetAll();
        Task<Customer> GetByIdAsync(string id);
        Task<int> customerNumber();
        List<ServiceRequest> GetServiceRequestsForThisCustomer(string id);
    }
}