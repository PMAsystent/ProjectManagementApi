using ProjectManagement.Core.Base.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProjectManagement.Core.Concrete.Identity.Dto;
using AutoMapper;
using System;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{
    public class RegisterUserCommand : IRequest<RegisterResponseDto>
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ApiUrl { get; set; }

        public RegisterUserCommand(RegisterUserBaseCommand baseCommand, string apiUrl) 
        {
            UserName = baseCommand.UserName;
            Email = baseCommand.Email;
            Password = baseCommand.Password;
            ApiUrl = apiUrl;
        }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterResponseDto>
    {
        private readonly IIdentityService _identityService;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
        {
            _identityService = identityService;
            _context = context;
            _mapper = mapper;
        }

        public async Task<RegisterResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var (Result, UserName, Email, Id) = await _identityService.RegisterUserAsync(request.Email,request.UserName, request.Password, request.ApiUrl);

            await _context.Users.AddAsync(new Domain.Entities.User { ApplicationUserId = Id, Email = Email, UserName = UserName }, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);


            return new RegisterResponseDto { UserName=UserName, Email=Email, Errors = new List<string>(Result.Errors) };
        }
    }
}
