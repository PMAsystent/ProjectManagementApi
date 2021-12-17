using ProjectManagement.Core.Base.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{
    public class ChangePasswordCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
    {
        private readonly IIdentityService _identityService;

        public ChangePasswordCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.ChangePasswordAsync(request.UserName,request.Email, request.OldPassword,request.NewPassword);
            return result.Succeeded;
        }
    }
}
