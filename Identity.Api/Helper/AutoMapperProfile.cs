using AutoMapper;
using Identity.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CustomerModel, Customer>();
            CreateMap<Customer, CustomerModel>();
        }
    }
}
