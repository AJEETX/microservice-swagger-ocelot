using Identity.Api.Model;
using Identity.Api.Persistence;
using Identity.Api.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace identity.api.test
{
    public class CustomerServiceTest
    {
        [Fact(DisplayName = "CustomerService_AddCustomer adds customer")]
        public async Task AddCustomer_adds_customer()
        {
            //given
            var customer = new Customer { UserName = "test" };
            var options = new DbContextOptionsBuilder<Database>().UseInMemoryDatabase(databaseName: "Add_to_database").Options;
            var logger = Mock.Of<ILogger<ICustomerService>>();

            //when
            using (var context = new Database(options))
            {
                var sut = new CustomerService(context, logger);
                await sut.AddCustomer(customer);
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
