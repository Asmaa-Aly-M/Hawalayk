using Hawalayk_APP.Models;

namespace Hawalayk_APP.Services
{
    public interface ICustomerRepository
    {
        
       
        List<Customer> GetAll();
        Customer GetById(string id);

    }
}