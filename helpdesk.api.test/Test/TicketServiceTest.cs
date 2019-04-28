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
        [Fact(DisplayName = "TicketService_GetTickets gets ticket")]
        public async Task GetTickets_gets_tickets()
        {
            //given
            var inputTicket = Testdata.GetTicket;
            var options = new DbContextOptionsBuilder<Database>().UseInMemoryDatabase(databaseName: "Get_from_database").Options;
            var logger = Mock.Of<ILogger<ITicketService>>();
            IEnumerable<Ticket> actual = null;
            using (var context = new Database(options))
            {
                //when
                var sut = new TicketService(context, logger);
                await sut.AddTicket(inputTicket);
                actual =await sut.GetTickets();

                //then
                Assert.Single(actual);
                Assert.Equal(inputTicket.Name, actual.Single().Name);
                Assert.Equal(inputTicket.Active, actual.Single().Active);
            }


        }
        [Fact(DisplayName = "TicketService GetTicketById gets ticket")]
        public async Task GetTicketById_gets_ticket()
        {
            //given
            var inputTicket = Testdata.GetTicket;
            var options = new DbContextOptionsBuilder<Database>().UseInMemoryDatabase(databaseName: "Get_from_database").Options;
            var logger = Mock.Of<ILogger<ITicketService>>();
            Ticket actual = null;
            using (var context = new Database(options))
            {
                //when
                var sut = new TicketService(context, logger);
                await sut.AddTicket(inputTicket);
                actual = await sut.GetTicketById(inputTicket.ID);

                //then
                Assert.Equal(inputTicket.Name, actual.Name);
                Assert.Equal(inputTicket.Active, actual.Active);
            }
        }

        [Fact(DisplayName = "TicketService AddTicket adds ticket")]
        public async Task AddTicket_adds_ticket()
        {
            //given
            var inputTicket = Testdata.GetTicket;
            var options = new DbContextOptionsBuilder<Database>().UseInMemoryDatabase(databaseName: "Add_to_database").Options;
            var logger = Mock.Of<ILogger<ITicketService>>();
            Ticket actual = null;
            //when
            using (var context = new Database(options))
            {
                var sut = new TicketService(context, logger);
                actual = await sut.AddTicket(inputTicket);
            }

            //then
            using (var context = new Database(options))
            {
                Assert.Equal(1, context.Tickets.Count());
                Assert.Equal(inputTicket.Name, context.Tickets.Single().Name);
                Assert.Equal(inputTicket.Active, context.Tickets.Single().Active);
            }
        }

        [Fact(DisplayName = "TicketService UpdateTicket updates ticket")]
        public async Task UpdateTicket_updates_ticket()
        {
            //given
            var inputTicket = Testdata.GetTicket;
            var options = new DbContextOptionsBuilder<Database>().UseInMemoryDatabase(databaseName: "Update_to_database").Options;
            var logger = Mock.Of<ILogger<ITicketService>>();
            string modifiedName = "modified";
            //when
            using (var context = new Database(options))
            {
                var sut = new TicketService(context, logger);
                var ticket2Update = await sut.AddTicket(inputTicket);
                ticket2Update.Name = modifiedName;
                await sut.UpdateTicket(ticket2Update.ID, ticket2Update);
            }

            //then
            using (var context = new Database(options))
            {
                Assert.Equal(1, context.Tickets.Count());
                Assert.Equal(modifiedName, context.Tickets.Single().Name);
            }
        }
        [Fact(DisplayName = "TicketService DeleteTicket updates ticket")]
        public async Task DeleteTicket_deletes_ticket()
        {
            //given
            var inputTicket = Testdata.GetTicket;
            var options = new DbContextOptionsBuilder<Database>().UseInMemoryDatabase(databaseName: "Delete_from_database").Options;
            var logger = Mock.Of<ILogger<ITicketService>>();
            //when
            using (var context = new Database(options))
            {
                var sut = new TicketService(context, logger);
                var ticket2Update = await sut.AddTicket(inputTicket);
                await sut.DeleteTicket(ticket2Update.ID);
            }

            //then
            using (var context = new Database(options))
            {
                Assert.Equal(0, context.Tickets.Count());
            }
        }
    }
}
