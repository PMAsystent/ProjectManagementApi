using MediatR;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.Concrete.Identity.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{

    public class SendChangeEmailAddressEmailCommand : IRequest<SendChangeAddressEmailDto>
    {
        public string Email { get; set; }

        public string NewEmail { get; set; }
    }

    public class SendChangeEmailAddressEmailCommandHandler : IRequestHandler<SendChangeEmailAddressEmailCommand, SendChangeAddressEmailDto>
    {
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;

        public SendChangeEmailAddressEmailCommandHandler(IIdentityService identityService, ICurrentUserService currentUserService)
        {
            _identityService = identityService;
            _currentUserService = currentUserService;
        }

        public async Task<SendChangeAddressEmailDto> Handle(SendChangeEmailAddressEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.SendChangeEmailAddressEmail(_currentUserService.UserId, request.Email, request.NewEmail);
            return result.Succeeded ? new SendChangeAddressEmailDto { IsSented = true } : new SendChangeAddressEmailDto { IsSented = false, Errors = result.Errors };
        }
    }
}
