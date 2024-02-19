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
        public void Create(Customer customer) //url الي هيجيب العميل الي اضفته
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
        public void Update(string id, Customer customer)
        {
            Customer Oldcustomer = _context.Customers.SingleOrDefault(c => c.Id == id);
            Oldcustomer.Id = id;
            Oldcustomer.FirstName = customer.FirstName;
            Oldcustomer.LastName = customer.LastName;
            Oldcustomer.UserName = customer.UserName;
            Oldcustomer.BirthDate = customer.BirthDate;
            Oldcustomer.Email = customer.Email;
            Oldcustomer.Address = customer.Address;
            _context.SaveChanges();
        }
        public void Delete(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }
}
