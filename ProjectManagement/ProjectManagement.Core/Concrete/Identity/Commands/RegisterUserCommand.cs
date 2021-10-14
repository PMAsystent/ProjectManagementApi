using ProjectManagement.Core.Base.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProjectManagement.Core.Concrete.Identity.Dto;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{
    public class RegisterUserCommand : IRequest<RegisterResponseDto>
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterResponseDto>
    {
        private readonly IIdentityService _identityService;

        public RegisterUserCommandHandler (IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<RegisterResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var (Result, UserName, Email) = await _identityService.RegisterUserAsync(request.Email,request.UserName, request.Password);
            return new RegisterResponseDto { UserName=UserName, Email=Email, Errors = new List<string>(Result.Errors) };
        }
    }
}
