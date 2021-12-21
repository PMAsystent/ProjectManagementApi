using MediatR;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.Concrete.Identity.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{
    public class SendResetPasswordEmailCommand : IRequest<SendResetPasswordEmailDto>
    {
        public string Email { get; set; }
    }

    public class SendResetPasswordEmailCommandHandler : IRequestHandler<SendResetPasswordEmailCommand, SendResetPasswordEmailDto>
    {
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;

        public SendResetPasswordEmailCommandHandler(IIdentityService identityService, ICurrentUserService currentUserService)
        {
            _identityService = identityService;
            _currentUserService = currentUserService;
        }

        public async Task<SendResetPasswordEmailDto> Handle(SendResetPasswordEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.SendResetPasswordEmail(_currentUserService.UserId, "test", request.Email);

            return result.Succeeded ? new SendResetPasswordEmailDto { IsSented = true } : new SendResetPasswordEmailDto { IsSented = false, Errors = result.Errors };
        }
    }
}
