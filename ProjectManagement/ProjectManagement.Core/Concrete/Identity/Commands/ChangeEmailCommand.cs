using ProjectManagement.Core.Base.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{
    public class ChangeEmailCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NewEmail { get; set; }
    }

    public class ChangeEmailCommandHandler : IRequestHandler<ChangeEmailCommand, bool>
    {
        private readonly IIdentityService _identityService;

        public ChangeEmailCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<bool> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.ChangeEmailAsync(request.UserName,request.Email, request.NewEmail);
            return result.Succeeded;
        }
    }
}
