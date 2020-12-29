using Domain.Base;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Task : AuditableEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime TargetDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        // TODO: read about conventions
        //public int? StepId { get; set; }
        public Step Step { get; set; }
        public Oversee Oversee { get; set; }
        public ICollection<Assign> Assigns { get; set; }
    }
}
