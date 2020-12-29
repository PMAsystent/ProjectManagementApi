using Domain.Base;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Boss : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public ICollection<ProjectManager> ProjectManagers { get; set; }
    }
}
