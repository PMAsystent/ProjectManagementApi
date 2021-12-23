using ProjectManagement.Core.Base.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{
    public class ConfirmEmailCommand : IRequest<bool>
    {
        public string UserId { get; set; }

        public string Token { get; set; }
    }

    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
    {
        private readonly IIdentityService _identityService;


        public ConfirmEmailCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.ConfirmEmailAsync(request.UserId, request.Token);
            return result.Succeeded;
        }
    }
}
