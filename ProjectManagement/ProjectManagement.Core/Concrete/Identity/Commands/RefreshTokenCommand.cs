using MediatR;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.Concrete.Identity.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{
    public class RefreshTokenCommand : IRequest<RefreshTokenResponseDto>
    {
        public string Email { get; set; }
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponseDto>
    {
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;

        public RefreshTokenCommandHandler(ICurrentUserService currentUserService, IIdentityService identityService)
        {
            _identityService = identityService;
            _currentUserService = currentUserService;
        }

        public async Task<RefreshTokenResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.RefreshToken(_currentUserService.UserId,request.Email);

            return new RefreshTokenResponseDto { Token = result.Token, Errors = new List<string>(result.Errors) }; ;
        }
    }
}
