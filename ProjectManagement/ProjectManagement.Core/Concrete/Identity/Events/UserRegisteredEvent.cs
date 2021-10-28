using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Base;
using MediatR;
using ProjectManagement.Core.Base.Model;
using ProjectManagement.Core.Concrete.Identity.Commands;

namespace ProjectManagement.Core.Concrete.Identity.Events
{
    public class UserRegisteredEvent : DomainEvent
    {
        public string ApplicationUserId { get; }

        public UserRegisteredEvent(string applicationUserId)
        {
            ApplicationUserId = applicationUserId;
        }
    }

    public class UserRegisteredEventHandler : INotificationHandler<DomainEventNotification<UserRegisteredEvent>>
    {
        private readonly IMediator _mediator;

        public UserRegisteredEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(DomainEventNotification<UserRegisteredEvent> notification,
            CancellationToken cancellationToken)
        {
            var createUserEntityCommand = new CreateUserEntityCommand()
            {
                ApplicationUserId = notification.DomainEvent.ApplicationUserId
            };

            await _mediator.Send(createUserEntityCommand, cancellationToken);
        }
    }
}