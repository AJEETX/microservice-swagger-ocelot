using AutoMapper;
using HelpDesk.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpDesk.Api.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Ticket, TicketModel>();
            CreateMap<TicketModel, Ticket>();
        }
    }
}
