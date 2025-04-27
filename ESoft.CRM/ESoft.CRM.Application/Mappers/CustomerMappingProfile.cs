using AutoMapper;
using ESoft.CRM.Application.Commands;
using ESoft.CRM.Domain.Entities;

namespace ESoft.CRM.Application.Mappers
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile() 
        {
            CreateMap<RegisterCustomerCommand, Customer>().ReverseMap();
        }
    }
}
