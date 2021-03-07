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
        public DateTime StartDate { get; set; }
        public DateTime TargetDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Step> Steps { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
