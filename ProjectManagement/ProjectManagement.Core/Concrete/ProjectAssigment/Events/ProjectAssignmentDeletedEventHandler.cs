using System.Threading;
using System.Threading.Tasks;
using Domain.Base;
using Domain.Events;
using MediatR;
using ProjectManagement.Core.Base.Model;
using ProjectManagement.Core.Concrete.ProjectAssigment.Commands;

namespace ProjectManagement.Core.Concrete.ProjectAssigment.Events
{
    public class ProjectAssignmentDeletedEventHandler : INotificationHandler<DomainEventNotification<ProjectAssignmentDeletedEvent>>
    {
        private readonly IMediator _mediator;

        public ProjectAssignmentDeletedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task Handle(DomainEventNotification<ProjectAssignmentDeletedEvent> notification, CancellationToken cancellationToken)
        {
            var deleteUserAssignmentsCommand = new DeleteUsersAssignmentsCommand()
            {
                ProjectId = notification.DomainEvent.ProjectId,
                UserId = notification.DomainEvent.UserId
            };

            await _mediator.Send(deleteUserAssignmentsCommand, cancellationToken);
        }
    }
}
