using Domain.Base;
using System;

namespace Domain.Entities
{
    public class Oversee : AuditableEntity
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
        public int ProjectManagerId { get; set; }
        public ProjectManager ProjectManager { get; set; }
    }
}
