using ProjectManagement.Core.Base.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagement.Core.Base.Model;
using ProjectManagement.Core.Concrete.Identity.Dto;
using AutoMapper;
using System.Linq;
using ProjectManagement.Core.UseCases.Users.Queries.Dto;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Collections.Generic;

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
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LoginUserCommandHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
        {
            _identityService = identityService;
            _context = context;
            _mapper = mapper;
        }

        public async Task<LoginResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var (Result, UserName, Email) = await _identityService.LoginUserAsync(request.Email, request.Password);

            var user = await _context.Users
                .ToListAsync(cancellationToken);




            if (Result.Succeeded)
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
