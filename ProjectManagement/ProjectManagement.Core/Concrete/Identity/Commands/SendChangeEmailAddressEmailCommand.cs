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

    public class SendChangeEmailAddressEmailCommand : IRequest<bool>
    {
        public string Email { get; set; }

        public string NewEmail { get; set; }
    }

    public class SendChangeEmailAddressEmailCommandHandler : IRequestHandler<SendChangeEmailAddressEmailCommand, bool>
    {
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;

        public SendChangeEmailAddressEmailCommandHandler(IIdentityService identityService, ICurrentUserService currentUserService)
        {
            _identityService = identityService;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(SendChangeEmailAddressEmailCommand request, CancellationToken cancellationToken)
        {
            await _identityService.SendChangeEmailAddressEmail(_currentUserService.UserId, "test", request.Email, request.NewEmail);
            return true;
        }
    }
}
