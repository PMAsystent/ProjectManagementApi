using ProjectManagement.Core.Base.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{
    public class LogoutUserCommand : IRequest<bool>
    {
        public string Email { get; set; }
    }

    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, bool>
    {
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;

        public LogoutUserCommandHandler(IIdentityService identityService, ICurrentUserService currentUserService)
        {
            _identityService = identityService;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            await _identityService.LogoutUserAsync(_currentUserService.UserId, request.Email);
            return true;
        }
    }
}
