namespace ESoft.CRM.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
