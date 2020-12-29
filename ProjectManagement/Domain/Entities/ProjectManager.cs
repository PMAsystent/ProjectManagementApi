using Domain.Base;

namespace Domain.Entities
{
    public class ProjectManager : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Boss Boss { get; set; }
        public Oversee Oversee { get; set; }
        public Assign Assign { get; set; }
    }
}
