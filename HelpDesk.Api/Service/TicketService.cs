using HelpDesk.Api.Model;
using HelpDesk.Api.Persistence;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Api.Service
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetTickets();
        Task<Ticket> GetTicketById(int id);
        Task<Ticket> AddTicket(Ticket ticket);
        Task<Ticket> UpdateTicket(int id, Ticket ticket);
        Task<Ticket> DeleteTicket(int id);
    }
    public class TicketService : ITicketService
    {
        private Database _context;
        ILogger<ITicketService> _logger;
        public TicketService(Database context, ILogger<ITicketService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Ticket> AddTicket(Ticket ticket)
        {
            if (ticket == null) return null; //always good to validate at start
            try
            {
                _logger.LogInformation($"Adding Ticket ...");
                await _context.Tickets.AddAsync(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                _logger.LogInformation($"Error Adding Ticket ...");
            }
            return ticket;
        }

        public async Task<Ticket> DeleteTicket(int id)
        {
            Ticket ticket = null;
            if (id < 1) return ticket;
            try
            {
                ticket = _context.Tickets.FirstOrDefault(i => i.ID == id);
                if (ticket == null) return ticket;
                _logger.LogInformation($"Deleteing Ticket ...");
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                _logger.LogInformation($"Error Deleteing Ticket ...");
            }

            return ticket;
        }

        public async Task<Ticket> GetTicketById(int id)
        {
            Ticket ticket = null;
            if (id < 1) return ticket;
            try
            {
                _logger.LogInformation($"Getting Ticket by id...");
                ticket = _context.Tickets.FirstOrDefault(Ticket => Ticket.ID == id);
                if (ticket == null) return ticket;
            }
            catch (Exception)
            {
                _logger.LogInformation($"Error Getting Ticket by id...");
            }
            return await Task.FromResult(ticket);
        }

        public async Task<IEnumerable<Ticket>> GetTickets()
        {
            IEnumerable < Ticket > tickets = null;
            try
            {
                _logger.LogInformation($"Getting Tickets...");
                tickets = _context.Tickets;
                if (tickets == null) return tickets;
            }
            catch (Exception)
            {
                _logger.LogInformation($"Error Getting Ticket ...");
            }
            return await Task.FromResult(tickets);
        }

        public async Task<Ticket> UpdateTicket(int id, Ticket ticket)
        {
            Ticket ticketInDB = null;
            if (id < 1 || ticket == null) return ticket;
            try
            {
                _logger.LogInformation($"Updating Ticket...");
                ticketInDB = _context.Tickets.FirstOrDefault(i => i.ID == id);
                if (ticketInDB == null) return ticketInDB;
                ticketInDB.Active = ticket.Active;
                ticketInDB.Name = ticket.Name;
                _context.Tickets.Update(ticketInDB);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                _logger.LogInformation($"Error Updating Ticket ...");
            }

            return ticketInDB;
        }
    }
}
