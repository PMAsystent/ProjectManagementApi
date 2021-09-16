using System;
using Domain.Base;

namespace Domain.Entities
{
    public class ProjectAssigment : AuditableEntity
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}