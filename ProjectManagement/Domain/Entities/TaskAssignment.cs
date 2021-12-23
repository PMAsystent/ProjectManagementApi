using Domain.Base;

namespace Domain.Entities
{
    public class TaskAssignment : AuditableEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
