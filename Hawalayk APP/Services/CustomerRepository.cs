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

        public async Task<List<Customer>> GetAll()
        {
            List<Customer> customers = await _context.Customers.ToListAsync();

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
                Governorate = customer.Address.Governorate.governorate_name_ar,
                City = customer.Address.City.city_name_ar,
                StreetName = customer.Address.StreetName
            };

        }

        public async Task<Customer> GetByIdAsync(string id)
        {
            Customer customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
            return customer;
        }

        public async Task<int> customerNumber()
        {
            int counter = await _context.Customers.CountAsync();
            return counter;
        }


    }
}
