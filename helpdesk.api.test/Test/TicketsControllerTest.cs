using AutoMapper;
using HelpDesk.Api.Service;
using Moq;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using HelpDesk.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HelpDesk.Api.Model;
using System.Threading.Tasks;

namespace helpdesk.api.test
{
    public class TicketsControllerTest
    {
        [Fact(DisplayName = "get all tickets")]
        public async Task Get_returns_all_tickets()
        {
            //given
            var moqMapper = new Mock<IMapper>();
            var moqTicketService = new Mock<ITicketService>();
            moqTicketService.Setup(m => m.GetTickets()).ReturnsAsync(Testdata.GetTestTickets);
            var logger = Mock.Of<ILogger<TicketsController>>();
            var controller = new TicketsController(moqTicketService.Object, logger, moqMapper.Object);

            //when
            var actual =await controller.Get();

            //then
            Assert.IsAssignableFrom<ActionResult<IEnumerable<TicketModel>>>(actual);
        }

        [Fact(DisplayName = "add a ticket")]
        public async Task Post_adds_the_supplied_ticket()
        {
            //given
            var moqMapper = new Mock<IMapper>();
            var moqTicketService = new Mock<ITicketService>();
            moqTicketService.Setup(m => m.AddTicket(It.IsAny<Ticket>())).ReturnsAsync(new Ticket());
            var logger = Mock.Of<ILogger<TicketsController>>();
            var controller = new TicketsController(moqTicketService.Object, logger, moqMapper.Object);

            //when
            var actual =await controller.Post(new TicketModel());

            //then
            Assert.IsType<CreatedAtActionResult>(actual);
        }
    }
}
