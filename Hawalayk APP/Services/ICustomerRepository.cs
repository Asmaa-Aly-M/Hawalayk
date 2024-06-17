using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Enums;
using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface ICustomerRepository
    {
        Task<int> customerNumber();
        Task<List<Customer>> GetAll();
        Task<Customer> GetByIdAsync(string id);
        Task<CustomerAccountDTO> GetCustomerAccountAsync(Customer customer);
        //Task<List<ServiceRequest>> GetServiceRequestsForThisCustomer(string customerID);
        Task<List<SearchAboutCraftsmanDTO>> searchAboutCraftsmen(string userId, CraftName craftName, string governorate);
    }
}