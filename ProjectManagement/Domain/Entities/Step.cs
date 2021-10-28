using Domain.Base;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Step : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}