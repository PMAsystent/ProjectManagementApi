using Domain.Base;

namespace Domain.Events
{
    public class ProjectAssignmentDeletedEvent : DomainEvent
    {
        public int UserId { get; }
        public int ProjectId { get; }

        public ProjectAssignmentDeletedEvent(int userId, int projectId)
        {
            UserId = userId;
            ProjectId = projectId;
        }
    }
}
