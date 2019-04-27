using Identity.Api.Model;
using Identity.Api.Persistence;
using Identity.Api.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace identity.api.test
{
    public class CustomerServiceTest
    {
        [Fact(DisplayName = "AddCustomer adds customer")]
        public void AddCustomer_adds_customer()
        {
            //given
            var customer = new Customer { UserName = "test" };
            var options = new DbContextOptionsBuilder<Database>().UseInMemoryDatabase(databaseName: "Add_to_database").Options;

            //when
            using (var context = new Database(options))
            {
                var sut = new CustomerService(context);
                sut.AddCustomer(customer);
            }

            //then
            using (var context = new Database(options))
            {
                Assert.Equal(1, context.Customers.Count());
                Assert.Equal(customer.UserName, context.Customers.Single().UserName);
            }
        }
    }
}
