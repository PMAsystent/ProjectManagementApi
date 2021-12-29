using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Core.Base.Exceptions;
using ProjectManagement.Core.Base.Interfaces;
using Task = Domain.Entities.Task;

namespace ProjectManagement.Core.UseCases.TaskAssignments.Commands.AssignUserToTask
{
    public class AssignUserToTaskCommandHandler : IRequestHandler<AssignUserToTaskCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AssignUserToTaskCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<Unit> Handle(AssignUserToTaskCommand request, CancellationToken cancellationToken)
        {
            if (!await TaskExists(request.TaskId))
            {
                throw new NotFoundException(nameof(Task), request.TaskId);
            }

            if (!await UserAssignedToProject(request.UserId, request.TaskId))
            {
                throw new Exception("User is not assigned to project.");
            }

            if (await UserAlreadyAssigned(request.UserId, request.TaskId))
            {
                throw new Exception("User is already assigned to task");
            }
            
            var taskAssigment = _mapper.Map<TaskAssignment>(request);
            taskAssigment.isActive = true;
            await _context.TaskAssignments.AddAsync(taskAssigment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }

        private async Task<bool> UserAlreadyAssigned(int userId, int taskId)
        {
            var taskAssignment = await _context.TaskAssignments
                .FirstOrDefaultAsync(a => a.UserId == userId && a.TaskId == taskId && a.isActive);
            
            return taskAssignment != null;
        }
        
        private async Task<bool> UserAssignedToProject(int userId, int taskId)
        {
            var projectId = await GetProjectId(taskId);
            var projectAssigment = await _context.ProjectAssignments
                .FirstOrDefaultAsync(a => a.UserId == userId && a.ProjectId == projectId);
            
            return projectAssigment != null;
        }
        
        private async Task<bool> TaskExists(int taskId) => await _context.Tasks.FindAsync(taskId) != null;
        
        private async Task<int> GetProjectId(int taskId)
        {
            var task = await _context.Tasks
                .Include(t => t.Step)
                .Include(t => t.Step.Project)
                .FirstOrDefaultAsync(t => t.Id == taskId);

            return task.Step.Project.Id;
        }
    }
}
