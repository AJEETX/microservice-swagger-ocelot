using HelpDesk.Api.Model;
using HelpDesk.Api.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Api.Service
{
    public interface IHelpdeskService
    {
        IEnumerable<Ticket> GetTickets();
        Ticket GetTicketById(int id);
        Ticket AddTicket(Ticket ticket);
        Ticket UpdateTicket(int id, Ticket ticket);
        Ticket DeleteTicket(int id);
    }
    public class HelpdeskService : IHelpdeskService
    {
        private Database _context;
        public HelpdeskService(Database context)
        {
            _context = context;
            //if (!_context.Tickets.Any())
            //{
            //    _context.Tickets.Add(new Ticket
            //    { Name = "First Sample Support Ticket", ID = 0, Active = true });
            //    _context.SaveChanges();
            //}
        }
        public Ticket AddTicket(Ticket ticket)
        {
            if (ticket == null) return null;

            _context.Tickets.Add(ticket);
            _context.SaveChanges();
            return ticket;
        }

        public Ticket DeleteTicket(int id)
        {
            var ticket = _context.Tickets.FirstOrDefault(i => i.ID == id);
            if (ticket == null) return null;
            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
            return ticket;
        }

        public Ticket GetTicketById(int id)
        {
            return _context.Tickets.FirstOrDefault(Ticket => Ticket.ID == id);
        }

        public IEnumerable<Ticket> GetTickets()
        {
            return _context.Tickets;
        }

        public Ticket UpdateTicket(int id, Ticket ticket)
        {
            var ticketInDB = _context.Tickets.FirstOrDefault(i => i.ID == id);
            if (ticketInDB == null) return null;
            ticketInDB.Active = ticket.Active;
            ticketInDB.Name = ticket.Name;
            _context.Tickets.Update(ticketInDB);
            _context.SaveChanges();
            return ticketInDB;
        }
    }
}
