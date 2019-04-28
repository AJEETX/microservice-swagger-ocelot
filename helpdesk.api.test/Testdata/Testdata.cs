using HelpDesk.Api.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace helpdesk.api.test
{
    class Testdata
    {
        internal static IEnumerable<Ticket> GetTestTickets
        {
            get
            {
                return new List<Ticket> { GetTicket };
            }
        }
        internal static Ticket GetTicket
        {
            get
            {
                return new Ticket {Name= "test ticket",Active= true };
            }
        }

        internal static IEnumerable<TicketModel> GetTestTicketModels
        {
            get
            {
                return new List<TicketModel> { GetTicketModel };
            }
        }
        internal static TicketModel GetTicketModel
        {
            get
            {
                return new TicketModel { Name = "test ticket", Active = true };
            }
        }
    }
}
