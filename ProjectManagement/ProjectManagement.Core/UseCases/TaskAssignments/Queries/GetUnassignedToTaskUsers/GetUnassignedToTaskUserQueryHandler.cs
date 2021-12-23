using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.TaskAssignments.Dto;
using ProjectManagement.Core.UseCases.TaskAssignments.ViewModels;

namespace ProjectManagement.Core.UseCases.TaskAssignments.Queries.GetUnassignedToTaskUsers
{
    public class GetUnassignedToTaskUserQueryHandler : IRequestHandler<GetUnassignedToTaskUserQuery, UnassignedUsersVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUnassignedToTaskUserQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UnassignedUsersVm> Handle(GetUnassignedToTaskUserQuery request, CancellationToken cancellationToken)
        {
            var activeAssignments = await _context.TaskAssignments
                .Where(ts => ts.TaskId == request.TaskId && ts.isActive)
                .Select(ts => ts.UserId)
                .ToListAsync(cancellationToken);

            var unassignedUsers = await _context.ProjectAssignments
                .Where(pa => !activeAssignments.Contains(pa.UserId))
                .Select(pa => pa.User)
                .ProjectTo<UnassignedUserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new()
            {
                UnassignedUser = unassignedUsers,
                Count = unassignedUsers.Count
            };
        }
    }
}
