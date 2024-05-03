using Hawalayk_APP.Context;
using Hawalayk_APP.DataTransferObject;
using Hawalayk_APP.Models;
using Microsoft.EntityFrameworkCore;

namespace Hawalayk_APP.Services
{

    public class CustomerRepository : ICustomerRepository

    {
        ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public List<Customer> GetAll()
        {
            List<Customer> customers = _context.Customers.ToList();

            return customers;


        }


        public async Task<CustomerAccountDTO> GetCustomerAccountAsync(Customer customer)
        {


            return new CustomerAccountDTO
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                UserName = customer.UserName,
                ProfilePic = Path.Combine("imgs/", customer.ProfilePicture),
                BirthDate = customer.BirthDate,
                PhoneNumber = customer.PhoneNumber,


            };

        }

        public async Task<Customer> GetByIdAsync(string id)
        {
            Customer customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            return customer;
        }

        public int customerNumber()
        {
            int counter = _context.Customers.Count();
            return counter;
        }


    }
}
