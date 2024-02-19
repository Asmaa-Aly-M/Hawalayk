using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface ICustomerRepository
    {
        void Create(Customer customer);
        void Delete(Customer customer);
        List<Customer> GetAll();
        Customer GetById(string id);
        void Update(string id, Customer customer);
    }
}