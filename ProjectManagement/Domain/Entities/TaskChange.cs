using System;
using Domain.Enums;

namespace Domain.Entities
{
    public class TaskChange
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public DateTime Date { get; set; } //AuditableEntity?
        public string ChangeDescription { get; set; }


        public int? TaskId { get; set; }
        public Task Task { get; set; }
        
        
    }
}