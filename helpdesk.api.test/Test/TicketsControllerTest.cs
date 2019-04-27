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

namespace helpdesk.api.test
{
    public class TicketsControllerTest
    {
        [Fact(DisplayName = "get all tickets")]
        public void Get_returns_all_tickets()
        {
            //given
            var moqMapper = new Mock<IMapper>();
            var moqTicketService = new Mock<ITicketService>();
            moqTicketService.Setup(m => m.GetTickets()).Returns(Testdata.GetTestTickets);
            var logger = Mock.Of<ILogger<TicketsController>>();
            var controller = new TicketsController(moqTicketService.Object, logger, moqMapper.Object);

            //when
            var actual = controller.Get();

            //then
            Assert.IsAssignableFrom<ActionResult<IEnumerable<TicketModel>>>(actual);
        }

        [Fact(DisplayName = "add a ticket")]
        public void Post_adds_the_supplied_ticket()
        {
            //given
            var moqMapper = new Mock<IMapper>();
            var moqTicketService = new Mock<ITicketService>();
            moqTicketService.Setup(m => m.AddTicket(It.IsAny<Ticket>())).Returns(new Ticket());
            var logger = Mock.Of<ILogger<TicketsController>>();
            var controller = new TicketsController(moqTicketService.Object, logger, moqMapper.Object);

            //when
            var actual = controller.Post(new TicketModel());

            //then
            Assert.IsType<CreatedAtActionResult>(actual);
        }
    }
}
