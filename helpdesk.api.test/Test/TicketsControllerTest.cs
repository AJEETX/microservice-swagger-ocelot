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
        [Fact(DisplayName = "TicketsController_get all tickets")]
        public async Task Get_returns_all_tickets()
        {
            //given
            var moqMapper = new Mock<IMapper>();
            moqMapper.Setup(m => m.Map<IEnumerable<TicketModel>>(It.IsAny<IEnumerable<Ticket>>())).Returns(Testdata.GetTestTicketModels);
            var moqTicketService = new Mock<ITicketService>();
            moqTicketService.Setup(m => m.GetTickets()).ReturnsAsync(Testdata.GetTestTickets);
            var logger = Mock.Of<ILogger<TicketsController>>();
            var controller = new TicketsController(moqTicketService.Object, logger, moqMapper.Object);

            //when
            var actual =await controller.Get();

            //then
            Assert.IsAssignableFrom<IActionResult>(actual);
            moqTicketService.Verify(v => v.GetTickets(), Times.Once);
            moqMapper.Verify(v => v.Map<IEnumerable<TicketModel>>(It.IsAny<IEnumerable<Ticket>>()), Times.Once);
        }
        [Fact(DisplayName = "TicketsController_get ticket")]
        public async Task Get_by_id_returns_the_ticket()
        {
            //given
            int ticketId = 1;
            var moqMapper = new Mock<IMapper>();
            moqMapper.Setup(m => m.Map<TicketModel>(It.IsAny<Ticket>())).Returns(Testdata.GetTicketModel);
            var moqTicketService = new Mock<ITicketService>();
            moqTicketService.Setup(m => m.GetTicketById(It.IsAny<int>())).ReturnsAsync(Testdata.GetTicket);
            var logger = Mock.Of<ILogger<TicketsController>>();
            var controller = new TicketsController(moqTicketService.Object, logger, moqMapper.Object);

            //when
            var actual = await controller.Get(ticketId);

            //then
            var okResult = Assert.IsType<OkObjectResult>(actual);
            var returnData = Assert.IsType<TicketModel>(okResult.Value);
            Assert.Equal(returnData.Name, Testdata.GetTicketModel.Name);
            moqTicketService.Verify(v => v.GetTicketById(It.IsAny<int>()), Times.Once);
            moqMapper.Verify(v => v.Map<TicketModel>(It.IsAny<Ticket>()), Times.Once);

        }
        [Fact(DisplayName = "TicketsController_add a ticket")]
        public async Task Post_adds_the_supplied_ticket()
        {
            //given
            var inputTicketModel = new TicketModel { Name = "Test Ticket", Active = true };
            var moqMapper = new Mock<IMapper>();
            var moqTicketService = new Mock<ITicketService>();
            moqTicketService.Setup(m => m.AddTicket(It.IsAny<Ticket>())).ReturnsAsync(Testdata.GetTicket);
            var logger = Mock.Of<ILogger<TicketsController>>();
            var controller = new TicketsController(moqTicketService.Object, logger, moqMapper.Object);

            //when
            var actual =await controller.Post(inputTicketModel);

            //then
            Assert.IsType<CreatedAtActionResult>(actual);
            moqTicketService.Verify(v => v.AddTicket(It.IsAny<Ticket>()), Times.Once);
        }
    }
}
