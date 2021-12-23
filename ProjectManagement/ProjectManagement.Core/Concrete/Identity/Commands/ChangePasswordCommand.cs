using ProjectManagement.Core.Base.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagement.Core.Concrete.Identity.Dto;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{
    public class ChangePasswordCommand : IRequest<ChangePasswordDto>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordDto>
    {
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;

        public ChangePasswordCommandHandler(IIdentityService identityService, ICurrentUserService currentUserService)
        {
            _identityService = identityService;
            _currentUserService = currentUserService;
        }

        public async Task<ChangePasswordDto> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.ChangePasswordAsync(_currentUserService.UserId,request.Email, request.OldPassword,request.NewPassword);
            return result.Succeeded ? new ChangePasswordDto { IsChanged = true } : new ChangePasswordDto { IsChanged = false, Errors = result.Errors };

        }
    }
}
