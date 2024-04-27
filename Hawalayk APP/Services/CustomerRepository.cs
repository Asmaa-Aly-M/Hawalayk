using Hawalayk_APP.Context;
using Hawalayk_APP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
        public Customer GetById(string id)
        {
            Customer customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            return customer;
        }

        public int customerNumber()
        {
            int counter = _context.Customers.Count();
            return counter;
        }


    }
}
