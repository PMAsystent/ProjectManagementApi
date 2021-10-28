using Domain.Base;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Task : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string TaskStatus { get; set; }
        public DateTime DueDate { get; set; }

        public int StepId { get; set; }
        public Step Step { get; set; }

        public ICollection<TaskAssignment> Assigns { get; set; }
        public ICollection<Subtask> Subtasks { get; set; }
    }
}