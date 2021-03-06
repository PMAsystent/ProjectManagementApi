using System;
using Domain.Base;
using Domain.Enums;

namespace Domain.Entities
{
    public class TaskChange : AuditableEntity
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        //public DateTime Date { get; set; } - Implemented by AuditableEntity
        public string ChangeDescription { get; set; }


        public int TaskId { get; set; }
        public Task Task { get; set; }
        
        
    }
}