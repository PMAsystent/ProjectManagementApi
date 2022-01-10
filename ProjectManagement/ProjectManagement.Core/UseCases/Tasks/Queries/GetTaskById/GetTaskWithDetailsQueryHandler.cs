using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.Base.Utils;
using ProjectManagement.Core.UseCases.Tasks.Dto;
using Task = Domain.Entities.Task;

namespace ProjectManagement.Core.UseCases.Tasks.Queries.GetTaskById
{
    public class GetTaskWithDetailsQueryHandler : IRequestHandler<GetTaskWithDetailsQuery, DetailedTaskDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IProgressPercentageService _progressPercentageService;

        public GetTaskWithDetailsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IProgressPercentageService progressPercentageService)
        {
            _mapper = mapper;
            _progressPercentageService = progressPercentageService;
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
            taskDto.AssignedUser =
                _mapper.Map<List<User>, ICollection<TaskAssignedUserDto>>(await GetAssignedUsers(request.TaskId));
            taskDto.Subtasks = await _context.Subtasks
                .Where(s => s.TaskId == task.Id)
                .ProjectTo<SubtaskDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            taskDto.ProgressPercentage = _progressPercentageService.GetProgressPercentageForTask(_mapper.Map<Task>(taskDto));

            return taskDto;
        }

        private async Task<List<User>> GetAssignedUsers(int taskId)
        {
            var taskAssignments = await _context.TaskAssignments
                .Where(a => a.TaskId == taskId && a.isActive)
                .ToListAsync();

            return await _context.Users
                .Where(u => taskAssignments.Select(a => a.UserId).Contains(u.Id))
                .ToListAsync();
        }
    }
}