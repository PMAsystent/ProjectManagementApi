using System;
using MediatR;
using ProjectManagement.Core.Base.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagement.Core.Concrete.Identity.Dto;
using System.Collections.Generic;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{
    public class CheckTokenCommand : IRequest<CheckTokenResponseDto>
    {
        public string Token { get; set; }
    }

    public class CheckTokenCommandHandler : IRequestHandler<CheckTokenCommand, CheckTokenResponseDto>
    {
        private readonly IIdentityService _identityService;

        public CheckTokenCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<CheckTokenResponseDto> Handle(CheckTokenCommand request, CancellationToken cancellationToken)
        {
            var (Result, UserName, Email) = await _identityService.CheckTokenAsync(request.Token);
            return new CheckTokenResponseDto { UserName = UserName, Email = Email, Errors = new List<string>(Result.Errors) };;
        }
    }
}
