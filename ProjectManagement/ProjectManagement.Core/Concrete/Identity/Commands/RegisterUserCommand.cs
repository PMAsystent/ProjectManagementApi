using ProjectManagement.Core.Base.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Events;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{
    public class RegisterUserCommand : IRequest<bool>
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IIdentityService _identityService;
        private readonly IDomainEventService _domainEventService;

        public RegisterUserCommandHandler(IIdentityService identityService, IDomainEventService domainEventService)
        {
            _identityService = identityService;
            _domainEventService = domainEventService;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var (result, userId) =
                await _identityService.RegisterUserAsync(request.Email, request.UserName, request.Password);

            await _domainEventService.Publish(new UserRegisteredEvent(userId));

            return result.Succeeded;
        }
    }
}