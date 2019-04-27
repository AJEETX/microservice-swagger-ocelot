using HelpDesk.Api.Model;
using HelpDesk.Api.Persistence;
using HelpDesk.Api.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace helpdesk.api.test.Test
{
    public class TicketServiceTest
    {
        [Fact(DisplayName = "AddTicket adds ticket")]
        public void AddTicket_adds_ticket()
        {
            //given
            var ticket = new Ticket { Name = "test", Active = true };
            var options = new DbContextOptionsBuilder<Database>().UseInMemoryDatabase(databaseName: "Add_to_database").Options;

            //when
            using (var context = new Database(options))
            {
                var sut = new TicketService(context);
                sut.AddTicket(ticket);
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
