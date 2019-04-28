using HelpDesk.Api.Model;
using HelpDesk.Api.Persistence;
using HelpDesk.Api.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace helpdesk.api.test.Test
{
    public class TicketServiceTest
    {
        [Fact(DisplayName = "AddTicket adds ticket")]
        public async Task AddTicket_adds_ticket()
        {
            //given
            var ticket = new Ticket { Name = "test", Active = true };
            var options = new DbContextOptionsBuilder<Database>().UseInMemoryDatabase(databaseName: "Add_to_database").Options;
            var logger = Mock.Of<ILogger<ITicketService>>();
            Ticket actual = null;
            //when
            using (var context = new Database(options))
            {
                var sut = new TicketService(context, logger);
                actual= await sut.AddTicket(ticket);
            }

            //then
            using (var context = new Database(options))
            {
                Assert.Equal(1, context.Tickets.Count());
                Assert.Equal(ticket.Name, context.Tickets.Single().Name);
                Assert.Equal(ticket.Active, context.Tickets.Single().Active);
            }
        }
    }
}
