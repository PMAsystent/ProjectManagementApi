using System.Collections.Generic;
using Domain.Base;

namespace Domain.Entities
{
    public class Customer : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }

        public ICollection<Project> Projects { get; set; }
        
    }
}