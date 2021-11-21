using Domain.Base;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Project : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsActive { get; set; }

        public ICollection<ProjectAssignment> Assigns { get; set; }
        public ICollection<Step> Steps { get; set; }
    }
}
