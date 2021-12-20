using MediatR;
using ProjectManagement.Core.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{
    public class SendResetPasswordEmailCommand : IRequest<bool>
    {
        public string Email { get; set; }
    }

    public class SendResetPasswordEmailCommandHandler : IRequestHandler<SendResetPasswordEmailCommand, bool>
    {
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;

        public SendResetPasswordEmailCommandHandler(IIdentityService identityService, ICurrentUserService currentUserService)
        {
            _identityService = identityService;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(SendResetPasswordEmailCommand request, CancellationToken cancellationToken)
        {
            await _identityService.SendResetPasswordEmail(_currentUserService.UserId, "test", request.Email);
            return true;
        }
    }
}
