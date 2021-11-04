using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Projects.Dto;
using ProjectManagement.Core.UseCases.Projects.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProjectManagement.Core.UseCases.Projects.Queries.GetMyProjectsList;

namespace ProjectManagement.Core.UseCases.Projects.Queries.GetProjects
{
    public class GetMyProjectsQueryHandler : IRequestHandler<GetMyProjectsQuery, MyProjectsListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetMyProjectsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MyProjectsListVm> Handle(GetMyProjectsQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _context.Users
                .SingleOrDefaultAsync(u => u.ApplicationUserId == request.CurrentUserGuid,cancellationToken);
            
            var myProjectsIds = await _context.ProjectAssignments
                .Where(a => a.UserId == currentUser.Id)
                .Select(a => a.ProjectId)
                .ToListAsync(cancellationToken);
            
            var myProjects = await _context.Projects
                .Where(p => myProjectsIds.Contains(p.Id))
                .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.Id)
                .ToListAsync(cancellationToken);

            // TODO: Calculate progress percentage of all projects
            
            return new()
            {
                ProjectsList = myProjects,
                Count = myProjects.Count
            };
        }
    }
}