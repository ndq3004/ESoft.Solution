﻿namespace ESoft.CRM.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
