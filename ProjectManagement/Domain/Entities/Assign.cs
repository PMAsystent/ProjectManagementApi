using Domain.Base;
using System;

namespace Domain.Entities
{
    public class Assign : AuditableEntity
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Task Task { get; set; }
        public ProjectManager ProjectManager { get; set; }

        // Konrad
        // public Role Role { get; set; }
        // public Employee Employee { get; set; }
    }
}
