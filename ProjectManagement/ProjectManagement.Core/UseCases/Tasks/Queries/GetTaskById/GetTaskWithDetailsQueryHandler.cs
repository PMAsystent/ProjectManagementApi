using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.Tasks.Dto;

namespace ProjectManagement.Core.UseCases.Tasks.Queries.GetTaskById
{
    public class GetTaskWithDetailsQueryHandler : IRequestHandler<GetTaskWithDetailsQuery, DetailedTaskDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTaskWithDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<DetailedTaskDto> Handle(GetTaskWithDetailsQuery request, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.FindAsync(request.TaskId);
            if (task == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Task), request.TaskId);
            }

            var taskDto = _mapper.Map<DetailedTaskDto>(task);
            taskDto.Assigns = await _context.TaskAssignments
                .Where(a => a.TaskId == task.Id)
                .ProjectTo<TaskAssignemntDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            taskDto.Subtasks = await _context.Subtasks
                .Where(s => s.TaskId == task.Id)
                .ProjectTo<SubtaskDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return taskDto;
        }
    }
}