using MediatR;
using ProjectManagement.Core.Base.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagement.Core.Concrete.Identity.Dto;
using System.Collections.Generic;
using AutoMapper;
using ProjectManagement.Core.UseCases.Users.Queries.Dto;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Core.Concrete.Identity.Commands
{
    public class CheckTokenCommand : IRequest<CheckTokenResponseDto>
    {
        public string Token { get; set; }
    }

    public class CheckTokenCommandHandler : IRequestHandler<CheckTokenCommand, CheckTokenResponseDto>
    {
        private readonly IIdentityService _identityService;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CheckTokenCommandHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
        {
            _identityService = identityService;
            _context = context;
            _mapper = mapper;
        }

        public async Task<CheckTokenResponseDto> Handle(CheckTokenCommand request, CancellationToken cancellationToken)
        {
            var (Result, UserName, Email) = await _identityService.CheckTokenAsync(request.Token);

            var users = await _context.Users
                .Where(u => u.UserName == UserName && u.Email == Email)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var user = users.FirstOrDefault();

            return new CheckTokenResponseDto { User = user, Errors = new List<string>(Result.Errors) };;
        }
    }
}
