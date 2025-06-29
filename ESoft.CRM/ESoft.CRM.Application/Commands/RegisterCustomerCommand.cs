using ESoft.CRM.Domain.Entities;
using ESoft.CRM.Domain.Interfaces;
using MediatR;

namespace ESoft.CRM.Application.Commands
{
    public class RegisterCustomerCommand : IRequest<Customer>, ICommand
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
