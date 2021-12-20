using ProjectManagement.Core.Base.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Events;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{
    public class ChangeEmailCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NewEmail { get; set; }

        public string Token { get; set; }
    }

    public class ChangeEmailCommandHandler : IRequestHandler<ChangeEmailCommand, bool>
    {
        private readonly IIdentityService _identityService;
        private readonly IDomainEventService _domainEventService;
        private readonly ICurrentUserService _currentUserService;

        public ChangeEmailCommandHandler(IIdentityService identityService, ICurrentUserService currentUserService, IDomainEventService domainEventService)
        {
            _identityService = identityService;
            _domainEventService = domainEventService;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.ChangeEmailAsync(_currentUserService.UserId,request.Email, request.NewEmail, request.Token);
            
            await _domainEventService.Publish(new UserEmailChangedEvent(request.Email, request.NewEmail));
            
            return result.Succeeded;
        }
    }
}
