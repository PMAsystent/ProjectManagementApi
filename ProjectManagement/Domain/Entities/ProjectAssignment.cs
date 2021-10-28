using System;
using Domain.Base;

namespace Domain.Entities
{
    public class ProjectAssignment : AuditableEntity
    {
        public int Id { get; set; }
        public string MemberType { get; set; }
        public string ProjectRole { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}