using ESoft.CRM.Application.Responses;
using ESoft.CRM.Domain.Entities;
using MediatR;

namespace ESoft.CRM.Application.Commands
{
    public class RegisterCustomerCommand : IRequest<Customer>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
