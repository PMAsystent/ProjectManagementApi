using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Users.Queries.Dto;
using ProjectManagement.Core.UseCases.Users.ViewsModels;

namespace ProjectManagement.Core.UseCases.Users.Queries.FindUser
{
    public class FindUserQueryHandler : IRequestHandler<FindUserQuery, UserVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FindUserQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<UserVm> Handle(FindUserQuery request, CancellationToken cancellationToken)
        {
            var usersByUserName = await _context.Users
                .Where(u => u.UserName.Contains(request.Term))
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            string term = request.Term.Contains("@") ? request.Term.Split("@")[0] : request.Term;
            
            var usersByUserEmail = await _context.Users
                .Where(u => u.Email.Contains(term) && !usersByUserName.Select(i => i.Id).Contains(u.Id))
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);


            var users = new List<UserDto>();
            
            users.AddRange(usersByUserEmail);
            users.AddRange(usersByUserName);
            
            return new()
            {
                Users = users,
                Count = users.Count
            };
        }
    }
}
