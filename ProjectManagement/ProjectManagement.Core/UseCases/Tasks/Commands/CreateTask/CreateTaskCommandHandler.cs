using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Interfaces;
using ProjectManagement.Core.UseCases.TaskAssignments.Commands.AssignUserToTask;
using ProjectManagement.Core.UseCases.Tasks.Dto;
using Task = Domain.Entities.Task;

namespace ProjectManagement.Core.UseCases.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, DetailedTaskDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateTaskCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DetailedTaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = _mapper.Map<Task>(request);
            await _context.Tasks.AddAsync(task, cancellationToken);
            
            await _context.SaveChangesAsync(cancellationToken);

            List<TaskAssignment> usersToAssign = new List<TaskAssignment>();

            foreach (var assignment in request.AssignedUsers)
            {
                if (!await UserAssignedToProject(assignment.UserId, request.StepId))
                {
                    throw new Exception("User is not assigned to project.");
                }
                
                usersToAssign.Add(new TaskAssignment()
                {
                    TaskId = task.Id,
                    UserId = assignment.UserId,
                    isActive = true
                });
            }
            await _context.TaskAssignments.AddRangeAsync(usersToAssign, cancellationToken);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            
            var taskDto = _mapper.Map<DetailedTaskDto>(task);
            taskDto.AssignedUser = _mapper.Map<List<User>, ICollection<TaskAssignedUserDto>>(await GetAssignedUsers(usersToAssign));
            taskDto.Subtasks = new List<SubtaskDto>();
            
            return taskDto;
        }
        
        private async Task<bool> UserAssignedToProject(int userId, int stepId)
        {
            var projectId = await GetProjectId(stepId);
            var projectAssigment = await _context.ProjectAssignments
                .FirstOrDefaultAsync(a => a.UserId == userId && a.ProjectId == projectId);
            
            return projectAssigment != null;
        }

        private async Task<int> GetProjectId(int stepId)
        {
            var step = await _context.Steps
                .Include(s => s.Project)
                .FirstOrDefaultAsync(s => s.Id == stepId);

            return step.Project.Id;
        }

        private async Task<List<User>> GetAssignedUsers(List<TaskAssignment> taskAssignments)
        {
            return await _context.Users
                .Where(u => taskAssignments.Select(a => a.UserId).Contains(u.Id))
                .ToListAsync();
        }
    }
}
