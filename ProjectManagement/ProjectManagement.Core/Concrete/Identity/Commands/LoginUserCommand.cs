﻿using ProjectManagement.Core.Base.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagement.Core.Concrete.Identity.Dto;
using AutoMapper;
using System.Linq;
using ProjectManagement.Core.UseCases.Users.Queries.Dto;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

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

            if (Result.Succeeded)
            {
                var users = await _context.Users
                    .Where(u => u.UserName == UserName && u.Email == Email)
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                var user = users.FirstOrDefault();

                return new LoginResponseDto { Token = Result.Token, User=user, Errors = Result.Errors };
            }
            else
            {
                return new LoginResponseDto { Token ="", Errors = Result.Errors};
            }
        }
    }
}
