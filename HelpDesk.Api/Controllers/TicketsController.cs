using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelpDesk.Api.Model;
using HelpDesk.Api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelpDesk.Api.Controllers
{
    /// <summary>
    /// ticket controller
    /// </summary>
    [Authorize("permission")]
    [Route("helpdesk/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        ITicketService _ticketService;
        ILogger<TicketsController> _logger;
        private IMapper _mapper;
        public TicketsController(ITicketService ticketService, ILogger<TicketsController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _ticketService = ticketService;
        }
        /// <summary>
        /// Get all tickets
        /// </summary>
        /// <returns>returns all tickets</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"getting tickets ...");
            var tickets = await _ticketService.GetTickets();
            if (tickets == null) NotFound();
            var ticketsModel = _mapper.Map<IEnumerable<TicketModel>>(tickets);
            return Ok(ticketsModel);
        }

        /// <summary>
        /// Get a specific ticket
        /// </summary>
        /// <param name="id"></param>
        /// <returns>returns the specified ticket</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            // No model state validation code here in dotnet ore 2.1, hooray!

            _logger.LogInformation($"getting ticket by id= {id} ...");
            var ticket = await _ticketService.GetTicketById(id);
            if (ticket == null) return NotFound();
            var ticketModel = _mapper.Map<TicketModel>(ticket);
            return Ok(ticketModel);
        }

        /// <summary>
        /// Post a new ticket
        /// </summary>
        /// <param name="ticketModel"></param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] TicketModel ticketModel)
        {
            var ticket = _mapper.Map<Ticket>(ticketModel);
            ticket = await _ticketService.AddTicket(ticket);
            if (ticket == null) return BadRequest();
            return CreatedAtAction("Get", new { id = ticket.ID }, ticket);
        }

        /// <summary>
        /// Put to modify a ticket
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ticketModel"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, [FromBody] TicketModel ticketModel)
        {
            if (id < 0) return BadRequest();

            var ticket = _mapper.Map<Ticket>(ticketModel);

            var updatedTicket =await _ticketService.UpdateTicket(id, ticket);

            if (updatedTicket == null) return NotFound();

            ticketModel = _mapper.Map<TicketModel>(updatedTicket);

            return Ok(ticketModel );
        }
        /// <summary>
        /// Delete a ticket
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id < 0) return BadRequest();

            var ticket = await _ticketService.DeleteTicket(id);
            if (ticket == null) return NotFound();
            return new NoContentResult();
        }
    }
}
