using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ESoft.CRM.Application.Events
{
    public class NewCustomerCreatedEvent
    {
        public Guid CustomerId { get; set; }
    }
}
