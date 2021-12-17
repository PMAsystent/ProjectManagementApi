using ProjectManagement.Core.Base.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{
    public class LogoutUserCommand : IRequest<bool>
    {
        public string UserName { get; set; }
    }

    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, bool>
    {
        private readonly IIdentityService _identityService;

        public LogoutUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<bool> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
           // await _identityService.LogoutUserAsync(request.UserName);
            return true;
        }
    }
}
