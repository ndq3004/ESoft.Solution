using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
