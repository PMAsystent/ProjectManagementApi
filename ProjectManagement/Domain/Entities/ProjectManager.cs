using Domain.Base;
using System.Collections.Generic;

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
        public ICollection<Oversee> Oversee { get; set; }
        public ICollection<Assign> Assign { get; set; }
    }
}
