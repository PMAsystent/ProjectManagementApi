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

namespace ProjectManagement.Core.UseCases.Tasks.Queries.GetTasks
{
    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, TaskVM>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTasksQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        public async Task<TaskVM> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _context.Tasks
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
