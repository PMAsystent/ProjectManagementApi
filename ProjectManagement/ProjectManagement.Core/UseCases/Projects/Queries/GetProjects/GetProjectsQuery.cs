using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Projects.Queries.GetProjects.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManagement.Core.UseCases.Projects.Queries.GetProjects
{
    public class GetProjectsQuery : IRequest<ProjectVm> { }
    public class GetTodosQueryHandler : IRequestHandler<GetProjectsQuery, ProjectVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProjectVm> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {

            return new ProjectVm
            {
                ProjectList = await _context.Projects
                .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.ID)
                .ToListAsync(cancellationToken)
            };
        }
    }
}
