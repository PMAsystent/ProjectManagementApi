using System.Threading;
using System.Threading.Tasks;
using Domain.Events;
using MediatR;
using ProjectManagement.Core.Base.Model;
using ProjectManagement.Core.Concrete.Identity.Commands;

namespace ProjectManagement.Core.Concrete.Identity.Events
{
    public class UserEmailChangedEventHandler : INotificationHandler<DomainEventNotification<UserEmailChangedEvent>>
    {
        private readonly IMediator _mediator;

        public UserEmailChangedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(DomainEventNotification<UserEmailChangedEvent> notification,
            CancellationToken cancellationToken)
        {
            var changeUserEmailCommand = new ChangeUserEmailCommand()
            {
                ApplicationUserEmail = notification.DomainEvent.ApplicationUserEmail,
                ApplicationUserNewEmail = notification.DomainEvent.ApplicationUserNewEmail,
            };

            await _mediator.Send(changeUserEmailCommand, cancellationToken);
        }
    }
}