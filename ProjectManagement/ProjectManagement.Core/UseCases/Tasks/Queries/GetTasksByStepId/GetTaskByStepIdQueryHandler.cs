using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Tasks.Dto;
using ProjectManagement.Core.UseCases.Tasks.ViewModels;

namespace ProjectManagement.Core.UseCases.Tasks.Queries.GetTasksByStepId
{
    public class GetTaskByStepIdQueryHandler : IRequestHandler<GetTasksByStepIdQuery, TaskVM>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        
        public GetTaskByStepIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<TaskVM> Handle(GetTasksByStepIdQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _context.Tasks
                .Where(t => t.StepId == request.StepId)
                .ProjectTo<TaskDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Id)
                .ToListAsync(cancellationToken);

            return new()
            {
                TaskList = tasks,
                Count = tasks.Count
            };
        }
    }
}
