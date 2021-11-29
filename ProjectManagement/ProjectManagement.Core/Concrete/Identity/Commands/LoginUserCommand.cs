using ProjectManagement.Core.Base.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagement.Core.Base.Model;
using ProjectManagement.Core.Concrete.Identity.Dto;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{
    public class LoginUserCommand : IRequest<LoginResponseDto>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponseDto>
    {
        private readonly IIdentityService _identityService;

        public LoginUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<LoginResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var (Result, UserName, Email) = await _identityService.LoginUserAsync(request.Email, request.Password);
            if(Result.Succeeded)
            {
                return new LoginResponseDto { Token = Result.Token, UserName = UserName, Email = Email };
            }
            else
            {
                return new LoginResponseDto { Token ="", UserName = UserName, Email = Email };
            }

        }
    }
}
