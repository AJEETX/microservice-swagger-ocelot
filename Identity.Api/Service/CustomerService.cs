using Identity.Api.Model;
using Identity.Api.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Service
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomers();
        bool Login(Login login);
        Customer AddCustomer(Customer customer);
        Customer DeleteCustomer(string userName);
    }
    public class CustomerService : ICustomerService
    {
        private Database _context;
        public CustomerService(Database context)
        {
            _context = context;
        }
        public Customer AddCustomer(Customer customer)
        {
            if (customer == null) return null;
            if (GetCustomer(customer.UserName) != null) return null;
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        public Customer DeleteCustomer(string email)
        {
            var customer = GetCustomer(email);
            if (customer == null) return null;
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return customer;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers;
        }

        public bool Login(Login login)
        {
            if (GetCustomer(login.UserName) == null) return false;
            return true;
        }
        private Customer GetCustomer(string userName)
        {
            return _context.Customers.FirstOrDefault(i => i.UserName == userName);
        }
    }
}