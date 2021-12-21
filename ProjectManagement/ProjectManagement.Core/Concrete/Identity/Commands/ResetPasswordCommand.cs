using MediatR;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.Concrete.Identity.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{
    public class ResetPasswordCommand : IRequest<ResetPasswordDto>
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string Token { get; set; }
    }

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordDto>
    {
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;

        public ResetPasswordCommandHandler(IIdentityService identityService, ICurrentUserService currentUserService)
        {
            _identityService = identityService;
            _currentUserService = currentUserService;
        }

        public async Task<ResetPasswordDto> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.ResetPasswordAsync(_currentUserService.UserId,request.Email, request.NewPassword, request.Token);
            return result.Succeeded ? new ResetPasswordDto { IsReseted = true } : new ResetPasswordDto { IsReseted = false, Errors = result.Errors };
        }
    }
}
