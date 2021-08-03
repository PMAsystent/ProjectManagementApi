using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Steps.Dto;
using ProjectManagement.Core.UseCases.Steps.ViewModels;

namespace ProjectManagement.Core.UseCases.Steps.Queries.GetStepByProjectId
{
    public class GetStepsByProjectIdQueryHandler : IRequestHandler<GetStepsByProjectIdQuery, StepVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        
        public GetStepsByProjectIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<StepVm> Handle(GetStepsByProjectIdQuery request, CancellationToken cancellationToken)
        {
            var steps = await _context.Steps
                .Where(s => s.ProjectId == request.ProjectId)
                .ProjectTo<StepDto>(_mapper.ConfigurationProvider)
                .OrderBy(s => s.Id)
                .ToListAsync(cancellationToken);

            return new()
            {
                StepList = steps,
                Count = steps.Count
            };
        }
    }
}