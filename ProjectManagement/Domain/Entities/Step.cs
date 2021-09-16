using Domain.Base;
using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Entities
{
    public class Step : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime TargetDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
