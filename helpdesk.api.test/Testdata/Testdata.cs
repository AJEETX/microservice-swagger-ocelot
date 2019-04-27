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
                return new List<Ticket> { new Ticket { ID = 1, Name = "test ticket", Active = true } };
            }
        }
    }
}
