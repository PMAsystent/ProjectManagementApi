using Domain.Base;

namespace Domain.Entities
{
    public class Employee : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public int AssignId { get; set; }
        public Assign Assign { get; set; }
    }
}