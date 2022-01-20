using Mc2.CrudTest.Presentation.Server.CommonDtos;
using Mc2.CrudTest.Presentation.Server.Core.Domain.Customers.Models;
using AutoMapper;

namespace Mc2.CrudTest.Presentation.Server.Common.Mappers
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerInfo, CreatCustomerDto>().ReverseMap();
        }

    }
}

