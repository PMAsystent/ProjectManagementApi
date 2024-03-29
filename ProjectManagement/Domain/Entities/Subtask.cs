using Domain.Base;

namespace Domain.Entities
{
    public class Subtask : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}