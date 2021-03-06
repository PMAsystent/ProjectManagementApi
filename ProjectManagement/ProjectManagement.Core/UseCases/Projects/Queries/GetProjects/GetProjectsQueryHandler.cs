using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Projects.Queries.GetProjects.Dto;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.UseCases.Projects.Queries.GetProjects
{
    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, ProjectVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProjectsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProjectVm> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _context.Projects
                .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.Id)
                .ToListAsync(cancellationToken);

            return new()
            {
                ProjectList = projects,
                Count = projects.Count
            };
        }
    }
}
