using Identity.Api.Model;
using Identity.Api.Persistence;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Service
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<bool> Login(Login login);
        Task<Customer> AddCustomer(Customer customer);
        Task<Customer> DeleteCustomer(string userName);
    }
    public class CustomerService : ICustomerService
    {
        private Database _context;
        ILogger<ICustomerService> _logger;
        public CustomerService(Database context, ILogger<ICustomerService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Customer> AddCustomer(Customer customer)
        {
            if (customer == null) return customer;
            try
            {
                _logger.LogInformation($"Adding Customer ...");
                if (GetCustomer(customer.UserName) != null) return customer;
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                _logger.LogInformation($"Error Adding Customer ...");
            }
            return customer;
        }

        public async Task<Customer> DeleteCustomer(string email)
        {
            Customer customer = null;
            if (string.IsNullOrEmpty(email)) return customer;
            try
            {
                _logger.LogInformation($"Deleting Customer ...");
                customer = GetCustomer(email);
                if (customer == null) return customer;
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                _logger.LogInformation($"Error Deleting Customer ...");
            }

            return customer;
        }

        public Task<IEnumerable<Customer>> GetCustomers()
        {
            IEnumerable<Customer> customers = null;
            try
            {
                _logger.LogInformation($"Getting Customers ...");
                customers = _context.Customers;
            }
            catch (Exception)
            {
                _logger.LogInformation($"Error Getting Customers ...");
            }
            return Task.FromResult(customers);
        }

        public Task<bool> Login(Login login)
        {
            bool loggedIn = false;
            try
            {
                _logger.LogInformation($"Logging Customer ...");
                if (GetCustomer(login.UserName) == null) return Task.FromResult( loggedIn);
                loggedIn = true;
            }
            catch (Exception)
            {
                _logger.LogInformation($"Error Logging Customer ...");
            }

            return Task.FromResult(loggedIn);
        }
        private Customer GetCustomer(string userName)
        {
            return _context.Customers.FirstOrDefault(i => i.UserName == userName);
        }
    }
}