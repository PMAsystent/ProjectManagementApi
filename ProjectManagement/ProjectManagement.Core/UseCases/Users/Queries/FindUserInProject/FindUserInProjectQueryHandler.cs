using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Users.Queries.Dto;
using ProjectManagement.Core.UseCases.Users.ViewsModels;

namespace ProjectManagement.Core.UseCases.Users.Queries.FindUserInProject
{
    public class FindUserInProjectQueryHandler : IRequestHandler<FindUserInProjectQuery, UserVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FindUserInProjectQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<UserVm> Handle(FindUserInProjectQuery request, CancellationToken cancellationToken)
        {
            var usersInProject = await _context.ProjectAssignments
                .Where(ta => ta.ProjectId == request.ProjectId)
                .Include(ta => ta.User)
                .Select(ta => ta.User)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var usersByUserName = usersInProject.Where(u => u.UserName.Contains(request.Term));

            string term = request.Term.Contains("@") ? request.Term.Split("@")[0] : request.Term;
            var rx = new Regex(@"^([^@]+)");
            
            var usersByUserEmail = usersInProject.Where(u => 
                !usersByUserName.Select(i => i.Id).Contains(u.Id) && rx.Match(u.Email).Success && 
                rx.Match(u.Email).Value.Contains(term)).ToList();

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
