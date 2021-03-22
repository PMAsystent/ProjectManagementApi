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

namespace ProjectManagement.Core.UseCases.Projects.Queries.GetProjectByCustomer
{
    public class GetProjectsByCustomerQueryHandler : IRequestHandler<GetProjectsByCustomerQuery, ProjectVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProjectsByCustomerQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ProjectVm> Handle(GetProjectsByCustomerQuery request, CancellationToken cancellationToken)
        {
            var projects = await _context.Projects
                .Where(p => p.CustomerId == request.CustomerId)
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
